using System.Text.RegularExpressions;

namespace Action_house
{
    class Validation
    {
        /// <summary>
        /// Static validation class with methods that validates all user input. Each method is titled what it does and follows a similar pattern,
        ///  either validating using Regex, TryParse or pattern matching. As well as applying value constraints.  
        /// </summary>

        static string DateTimeSeparator = "";

        static string HourMinuteSeparator = ":";
        static string DateSeparator = "/";

        public static int getOption(string prompt, int min, int max)
        {
            while (true)
            {
                var input = GetInput(prompt);

                if (int.TryParse(input, out int result))
                {
                    if (min <= result && result <= max)
                        return result - 1;
                    else
                        InvalidError("option");
                }
                else InvalidError("option");

            }
        }

        public static string GetInput(string prompt)
        {
            Console.WriteLine("{0}: ", prompt);
            Console.Write("> ");
            return Console.ReadLine();

        }

        public static bool GetInputBool(string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                return true;
            }
            else
                return false;
        }

        //overload get interger method to allow to pass an addional error message
        public static string GetInteger(string prompt, string obj)
        {
            while (true)
            {
                var response = GetInput(prompt);
                int integer;
                if (int.TryParse(response, out integer))
                {
                    if (integer > 0)
                    {
                        return integer.ToString();
                    }
                    else
                        Console.Write($"{obj} must be greater than 0");

                }
                else
                    Console.Write($"Invalid Input: {obj} must be a positive integer");

            }
        }


        public static string GetUnitNum(string prompt, string obj)
        {
            while (true)
            {
                var response = GetInput(prompt);
                int integer;
                if (int.TryParse(response, out integer))
                {
                    if (integer >= 0)
                    {
                        return integer.ToString();
                    }
                    else
                        Console.Write($"{obj} must be greater than 0");

                }
                else
                    Console.Write($"Invalid Input: {obj} must be a positive integer");

            }
        }

        public static string GetEmail(string prompt)
        {
            while (true)
            {
                var response = GetInput(prompt);
                string email_regex =
                    "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])";

                Match m = Regex.Match(response, email_regex);
                if (m.Success)
                {
                    return response;
                }
                else
                    InvalidError("email");
            }
        }

        public static string GetPwd(string prompt)
        {
            while (true)
            {
                var response = GetInput(prompt);
                string pwd_regex =
                    //using regex i wrote 
                    "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#`?!<>\"{}+/()^;=~'_@$,%^&*-]).{8,}$";

                Match m = Regex.Match(response, pwd_regex);
                if (m.Success)
                {
                    return response;
                }
                else
                    InvalidError("password");
            }
        }

        public static string GetLetters(string prompt)
        {
            while (true)
            {
                var response = GetInput(prompt);
                string letters_regex =
                    "^[^.]*$";

                Match m = Regex.Match(response, letters_regex);
                if (m.Success)
                {
                    return response;
                }
                else
                    InvalidError(prompt);
            }
        }

        public static string GetLetters(string prompt, string obj)
        {
            while (true)
            {
                var response = GetInput(prompt);
                string letters_regex = "^[^.]*$"; //dont match numbers 

                Match m = Regex.Match(response, letters_regex);
                if (m.Success)
                {
                    return response;
                }
                else
                    InvalidError(obj);
            }
        }

        public static string GetStreetSuff(string prompt)
        {
            while (true)
            {
                var response = GetInput(prompt);

                string[] suffixes = { "rd", "st", "dr", "blvd", "drive", "road", "avenue", "street", "blvd", "place", "terrace", "trail", "way", "lane", "circle", "centre", "junction", "point", "pass", "center", "park" };


                List<string> matches = new List<string>();

                for (int x = 0; x < suffixes.Length; x++)
                {

                    if (string.Equals(suffixes[x], response.ToLower()))
                    {
                        matches.Add(response);
                        return response;
                    }
                }

                if (matches.Count() == 0)
                {
                    InvalidError(prompt);
                }
            }
        }

        public static string GetYesNo(string prompt)
        {
            while (true)
            {
                var response = GetInput(prompt);

                string[] valid = { "YES", "NO" };

                List<string> matches = new List<string>();

                for (int x = 0; x < valid.Length; x++)
                {

                    if (string.Equals(valid[x], response.ToUpper()))
                    {
                        matches.Add(response);
                    }
                }

                int matchesCount = matches.Count();

                if (matchesCount > 0)
                {
                    return response;
                }
                else InvalidError("input (yes or no)");
            }
        }

        public static string GetPostcode(string prompt)
        {
            while (true)
            {
                var response = GetInput(prompt);

                string num_regex = "^[1000-9999]{4}$";

                Match m = Regex.Match(response, num_regex);
                if (m.Success)
                {
                    return response;
                }
                else
                    InvalidError(prompt);
            }
        }

        public static string GetState(string prompt)
        {
            while (true)
            {
                var response = GetInput(prompt);
                response = response.ToUpper();

                string[] states =
                { "NSW", "QLD", "VIC", "TAS", "SA", "WA", "NT", "ACT" };

                for (int x = 0; x <= states.Length; x++)
                {
                    if (string.Equals(states[x], response))
                    {
                        return response;
                    }
                }

                //this may not work just test it
                InvalidError(prompt);
            }
        }

        public static double GetPrice(string prompt)
        {

            while (true)
            {
                var response = GetInput(prompt);

                int integer;

                double Double;

                string PriceRegex = "[$[0-9]+.[0-9]+";

                Match m = Regex.Match(response, PriceRegex);

                if (m.Success)
                {
                    string doubleString = response.Remove(0, 1);

                    if (double.TryParse(doubleString, out Double))
                        return Double;
                    else
                        InvalidError("currency");

                    if (Double < 0)
                    {
                        Console.WriteLine("Invalid input: Currency must be greater than 0");
                    }

                }
                else InvalidError("currency");
            }
        }


        public static string GetProductDesc(string prompt, string productName)
        {

            while (true)
            {
                string response = GetInput(prompt);

                string desc_regex = $"({productName})";

                Match m = Regex.Match(response, desc_regex);

                if (m.Success)
                {
                    Console.WriteLine(m);
                    InvalidError("description");
                }
                else
                    return response;
            }


        }


        // public static DateTime GetDateTime(string prompt)
        // {
        //     while (true)
        //     {
        //         string input = GetInput(prompt);

        //         string[] DateAndTime = input.Split(DateTimeSeparator);

        //         string Date = "";

        //         Date = DateAndTime[0].ToString();

        //         string[] DateSepr = Date.Split(DateSeparator);

        //         string Time = "";

        //         Time = DateAndTime[1].ToString();

        //         string[] TimeSepr = Time.Split(HourMinuteSeparator);

        //         if (int.TryParse(DateSepr[2], out int Year))
        //         {
        //             if (int.TryParse(DateSepr[1], out int Month))
        //             {
        //                 if (int.TryParse(DateSepr[0], out int Day))
        //                 {
        //                     if (int.TryParse(TimeSepr[0], out int Hour))
        //                     {
        //                         if (int.TryParse(TimeSepr[1], out int Minute))
        //                         {
        //                             DateTime DateTimeInput = new DateTime(Year, Month, Day, Hour, Minute, 00);
        //                             return DateTimeInput;
        //                         }
        //                     }
        //                 }
        //             }
        //         }
        //         else InvalidError("delivery window"); ;
        //     }
        // }

        public static DateTime GetDateTime(string prompt)
        {
            while (true)
            {
                string input = GetInput(prompt);

                DateTime DTinput = new DateTime();

                if (DateTime.TryParse(input, out DTinput))
                {
                    return DTinput;
                }
                else InvalidError("delivery window"); ;
            }
        }

        /// <summary>
        /// Error Displays  
        /// </summary>
        public static void Error(string msg)
        {
            Console.WriteLine($"Invalid {msg}, please try again");
            Console.WriteLine();
        }

        public static void InvalidError(string obj)
        {
            Console.WriteLine("\n");
            Console.WriteLine($"Invalid Input: The supplied value is not a valid {obj}");
            Console.WriteLine("\n");
        }
    }

}


