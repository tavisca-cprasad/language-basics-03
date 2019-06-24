using System;
using System.Linq;

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

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
        	// Add your code here.
        	int n = protein.Length;
            int[] ans = new int[dietPlans.Length];
            for(int i=0; i<dietPlans.Length; i++){
                var diet = dietPlans[i];
                ans[i] = solve(protein, carbs, fat, diet);
            }
            return ans;
            throw new NotImplementedException();
        }

        public static int solve(int[] protein, int[] carbs, int[] fat, string diet){
            int[] calorie = new int[fat.Length];
            for(int i=0; i<fat.Length; i++){
                calorie[i] = fat[i] * 9 + (carbs[i] + protein[i]) * 5;
            }
            List<int> food = new List<int>();
            for(int i=0; i<protein.Length; i++){
                food.Add(i);
            }

            foreach(char ch in diet){
                if(ch >= 'A' && ch <= 'Z'){
                    if(ch == 'P') food = foodMax(food, protein);
                    if(ch == 'C') food = foodMax(food, carbs);
                    if(ch == 'F') food = foodMax(food, fat);
                    if(ch == 'T') food = foodMax(food, calorie);
                }
                else{
                    if(ch == 'p') food = foodMin(food, protein);
                    if(ch == 'c') food = foodMin(food, carbs);
                    if(ch == 'f') food = foodMin(food, fat);
                    if(ch == 't') food = foodMin(food, calorie);
                }

                if(food.Count == 1) return food[0];
            }

            return food[0];
        }

        public static List<int> foodMax(List<int> idx, int[] a){
            var maxval = a[idx[0]];
            for(int i=1; i<idx.Count; i++)
                maxval = Math.Max(maxval, a[idx[i]]);
            
            List<int> toremove = new List<int>();
            foreach(var i in idx){
                if(a[i] != maxval) 
                    toremove.Add(i);
            }

            foreach(var i in toremove)
                idx.Remove(i);

            return idx;
        }

        public static List<int> foodMin(List<int> idx, int[] a){
            var maxval = a[idx[0]];
            for(int i=1; i<idx.Count; i++)
                maxval = Math.Min(maxval, a[idx[i]]);
            
            List<int> toremove = new List<int>();
            foreach(var i in idx){
                if(a[i] != maxval) 
                    toremove.Add(i);
            }

            foreach(var i in toremove)
                idx.Remove(i);

            return idx;
        }
    }
}
