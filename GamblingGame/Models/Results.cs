using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace GamblingGame.Models
{
    public static class Results
    {
        public static int Spin()
        {
            var random = new Random();
            var x = random.Next(0, 37);
            return x;
        }

        public static string WinResults(int spinValue, List<int> boxValues, Game obj)
        {
            if (!obj.Red && !obj.Black && !obj.Even && !obj.High && !obj.Odd && 
                !obj.FirstTwelve && !obj.SecondTwelve && !obj.ThirdTwelve && !obj.Low && !obj.High)
            {

                foreach (var value in boxValues)
                {
                    if (value == spinValue) return $"You won!The ball landed on {spinValue}!You walk away with {GenerateEarnings(obj.BetAmount,true,"Box")}";
                }

                return $"Sadly you lost, the ball landed on {spinValue}. You are out {GenerateEarnings(obj.BetAmount,false,"Box")}";
            }

            if (obj.Red || obj.Black)
            {
                if (obj.Red)
                {
                    var redValues = new int[] {1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36};
                    foreach (var value in redValues)
                    {
                        if (spinValue == value)
                        {
                            return $"You Win!, You placed your bets on Red and the ball landed on {spinValue}! You win {GenerateEarnings(obj.BetAmount,true,"RedBlack")}";
                        }
                    }

                    return $"You Loose! You placed you bets on Red and Lost {GenerateEarnings(obj.BetAmount,false,"RedBlack")}";
                }
                else
                {
                    var blackValues = new int[] {2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35};
                    foreach (var value in blackValues)
                    {
                        if (spinValue == value)
                        {
                            return $"You Win!, You placed your bets on Black and the ball landed on {spinValue}! You won {GenerateEarnings(obj.BetAmount, true, "RedBlack")}";
                        }
                    }

                    return
                        $"You Loose, Ball landed on{spinValue}! You placed you bets on Black and Lost You lost {GenerateEarnings(obj.BetAmount, false, "RedBlack")}";
                }

            }

            if (obj.Even || obj.Odd)
            {
                if (obj.Even)
                {
                   
                        if (spinValue % 2 == 0)
                        {
                            return $"You Win!, You placed your bets on Even and the ball landed on {spinValue}! You won {GenerateEarnings(obj.BetAmount, true, "EvenOdd")}";
                        }

                        return $"You Loose, Ball landed on{spinValue}! You placed you bets on Even and Lost {GenerateEarnings(obj.BetAmount, false, "EvenOdd")}";
                }
                else
                {


                    if (spinValue % 2 != 0) 
                    {
                        return $"You Win!, You placed your bets on Odd and the ball landed on {spinValue}! You won {GenerateEarnings(obj.BetAmount, true, "EvenOdd")}";
                    }

                    return $"You Loose, Ball landed on{spinValue}! You placed you bets on Odd and Lost {GenerateEarnings(obj.BetAmount, false, "EvenOdd")}";
                }
            }

            if (obj.FirstTwelve || obj.SecondTwelve || obj.ThirdTwelve)
            {
                if (obj.FirstTwelve && obj.SecondTwelve)
                {

                    if (spinValue > 0 && spinValue <= 24)
                    {
                        return
                            $"You Win!, You placed your bets on both First twelve and Second Twelve and the ball landed on {spinValue}! You won {GenerateEarnings(obj.BetAmount, true, "Dozen")} This does not subtract your losses for having two dozens";
                    }

                    return
                        $"You loose the ball landed on {spinValue}, You placed bets on first twelve and second twelve and lost {GenerateEarnings(obj.BetAmount, false, "EvenOdd") *2}.";
                }

                if (obj.FirstTwelve && obj.ThirdTwelve)
                {
                    if (spinValue > 0 && spinValue <= 12 || spinValue > 24 && spinValue <= 36)
                    {
                        return
                            $"You Win!, You placed your bets on both First twelve and third Twelve and the ball landed on {spinValue}! You won {GenerateEarnings(obj.BetAmount, true, "Dozen")} This does not subtract your losses for having two dozens";
                    }

                    return
                        $"You loose the ball landed on {spinValue}, You placed bets on first twelve and second twelve and lost.";
                }

                if (obj.SecondTwelve && obj.ThirdTwelve)
                {
                    if (spinValue >= 12 && spinValue <= 36)
                    {
                        return
                            $"You Win!, You placed your bets on both First twelve and third Twelve and the ball landed on {spinValue}! You won {GenerateEarnings(obj.BetAmount, true, "Dozen")} This does not subtract your losses for having two dozens";
                    }

                    return
                        $"You loose the ball landed on {spinValue}, You placed bets on first twelve and second twelve and lost {GenerateEarnings(obj.BetAmount, false, "EvenOdd") * 2}.";
                }

                if (obj.FirstTwelve && !obj.SecondTwelve && !obj.ThirdTwelve)
                {
                    if (spinValue > 0 && spinValue <= 12)
                    {
                        return $"You Win!, You placed your bets on First twelve  and the ball landed on {spinValue}!You won {GenerateEarnings(obj.BetAmount, true, "Dozen")}";
                    }

                    return $"You loose the ball landed on {spinValue}, You placed bets on first twelve and lost {GenerateEarnings(obj.BetAmount, false, "EvenOdd")}.";
                }

                if (obj.SecondTwelve && !obj.FirstTwelve && !obj.ThirdTwelve)
                {
                    if (spinValue > 11 && spinValue <= 24)
                    {
                        return $"You Win!, You placed your bets on Second twelve and the ball landed on {spinValue}!You won {GenerateEarnings(obj.BetAmount, true, "Dozen")}";
                    }

                    return $"You loose the ball landed on {spinValue}, You placed bets on second twelve and lost {GenerateEarnings(obj.BetAmount, false, "EvenOdd")}..";
                }

                if (obj.ThirdTwelve && !obj.SecondTwelve && !obj.FirstTwelve)
                {
                    if (spinValue > 24 && spinValue <= 36)
                    {
                        return $"You Win!, You placed your bets on third twelve and the ball landed on {spinValue}!You won {GenerateEarnings(obj.BetAmount, true, "Dozen")}";
                    }

                    return $"You loose the ball landed on {spinValue}, You placed bets on third twelve and lost {GenerateEarnings(obj.BetAmount, false, "EvenOdd")}..";
                }
            }

            if (obj.High || obj.Low)
            {
                if (obj.High)
                {

                    if (spinValue > 18 && spinValue <= 36)
                    {
                        return $"You Win!, You placed your bets on High and the ball landed on {spinValue}! You won {GenerateEarnings(obj.BetAmount, true, "HighLow")}";
                    }

                    return
                        $"You Loose, Ball landed on{spinValue}! You placed you bets on High and Lost {GenerateEarnings(obj.BetAmount, false, "HighLow")}";
                }
                if(obj.Low)
                {
                    if (spinValue > 0 && spinValue <= 18)
                    {
                        return $"You Win!, You placed your bets on Low and the ball landed on {spinValue}! You won {GenerateEarnings(obj.BetAmount, true, "HighLow")}"; 
                    }

                    return
                        $"You Loose, Ball landed on{spinValue}! You placed you bets on Low and Lost {GenerateEarnings(obj.BetAmount, false, "HighLow")}";
                }
            }

            return "Invalid response";

        }

        public static double GenerateEarnings(double amountBet,bool won ,string betType)
        {
            if (betType == "EvenOdd" || betType == "HighLow" || betType == "RedBlack")
            {
                if (won)
                {
                    return amountBet * 2;
                }

                return amountBet;
            }

            if (betType == "Dozen")
            {
                if (won)
                {
                    return amountBet += amountBet *2;
                }

                return amountBet;
            }
            if (betType == "Box")
            {
                if (won)
                {
                    return  amountBet * 35;
                }

                return amountBet;
            }
            return 0;
        }
    }
}
