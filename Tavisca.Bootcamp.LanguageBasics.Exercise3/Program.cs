using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 },
                new[] { 2, 8 },
                new[] { 5, 2 },
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" },
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 },
                new[] { 2, 8, 5, 1 },
                new[] { 5, 2, 4, 4 },
                new[] { "tFc", "tF", "Ftc" },
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 },
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 },
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 },
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" },
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }
        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }
        public static int Method1(int[] arr, List<int> allowed, string Check = "max")
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            if (Check.Equals("max"))
            {
                for (int i = 0; i < allowed.Count; i++)
                {
                    if (max < arr[allowed[i]])
                    {
                        max = arr[allowed[i]];
                    }
                }
                return max;
            }
            else
            {
                for (int i = 0; i < allowed.Count; i++)
                {
                    if (min > arr[allowed[i]])
                    {
                        min = arr[allowed[i]];
                    }
                }
                return min;
            }

        }

        public static List<int> Method2(int[] arr, List<int> allowed, int val)
        {
            List<int> tempAllowed = new List<int>();
            for (int i = 0; i < allowed.Count; i++)
            {
                if (arr[allowed[i]] == val)
                {
                    tempAllowed.Add(allowed[i]);
                }
            }
            return tempAllowed;

        }
        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
           
            int[] calorie = new int[fat.Length];
            for (int i = 0; i < fat.Length; i++)
            {
                calorie[i] = fat[i] * 9 + protein[i] * 5 + carbs[i] * 5;
            }
            int[] exp = new int[dietPlans.Length];
            for (int i = 0; i < dietPlans.Length; i++)
            {
                List<int> Adjust = new List<Int32>();
                char[] plans = dietPlans[i].ToCharArray();
                for (int k = 0; k < fat.Length; k++)
                {
                    Adjust.Add(k);

                }
                for (int j = 0; j < plans.Length; j++)
                {
                    if (plans[j].Equals('P'))
                    {
                        int max = Method1(protein, Adjust);
                        Adjust = Method2(protein, Adjust, max);

                    }
                    else if (plans[j].Equals('p'))
                    {
                        int min = Method1(protein, Adjust, "min");
                        Adjust = Method2(protein, Adjust, min);
                    }
                    else if (plans[j].Equals('C'))
                    {
                        int max = Method1(carbs, Adjust);
                        Adjust = Method2(carbs, Adjust, max);
                    }
                    else if (plans[j].Equals('c'))
                    {
                        int min = Method1(carbs, Adjust, "min");
                        Adjust = Method2(carbs, Adjust, min);
                    }
                    else if (plans[j].Equals('F'))
                    {
                        int max = Method1(fat, Adjust);
                        Adjust = Method2(fat, Adjust, max);
                    }
                    else if (plans[j].Equals('f'))
                    {
                        int min = Method1(fat, Adjust, "min");
                        Adjust = Method2(fat, Adjust, min);
                    }
                    else if (plans[j].Equals('T'))
                    {
                        int max = Method1(calorie, Adjust);
                        Adjust = Method2(calorie, Adjust, max);
                    }
                    else if (plans[j].Equals('t'))
                    {
                        int min = Method1(calorie, Adjust, "min");
                        Adjust = Method2(calorie, Adjust, min);
                    }
                }
                exp[i] = Adjust[0];
            }
            return exp;
        }


    }
}

