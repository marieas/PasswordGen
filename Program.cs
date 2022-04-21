using System;

namespace UndervisningUke1
{
    class Program
    {
        static Random Random = new Random();
        static void Main(string[] args)
        {
            if (!IsValid(args)) // Om argumentene ikke er i formatet vi ønsker, printes hjelpeteksten og programmet avsluttes
            {
                PrintHelpTekst();
                return;
            }
            var passwordLength = args[0];
            var pattern = args[1];

            int length = Convert.ToInt32(passwordLength);
            // siden args er et string array er vi nødt til å konvertere det til en int
            // slik at vi får sammenlignet det med verdien pattern.Length som er type int

            while (pattern.Length < length) // så lenge patternet ikke oppfyller lengdekravet til passordet legger vi til liten l som fyll
            {
               pattern = pattern.PadRight(length, 'l');
            }

            string password = "";
            foreach(var character in pattern)
            { // for hver av tegnene i pattern sjekker vi hva verdien er, og henter ut ønsket verdi og legger til i passordet vårt
                if (character == 'l')
                {
                    password += GetRandomLowerCaseLetter();
                }
                else if(character == 'L')
                {
                    password += GetRandomUpperCaseLetter();
                }
                else if(character == 's')
                {
                    password += WriteRandomSpecialCharacter();
                }
                else
                {
                    password += GetRandomDigit();
                }

            }

            Console.WriteLine(password);          
        }

        private static bool IsValid(string[] args) //metoden returnerer true dersom argumentene oppfyller ønskede krav
        {
            if (args.Length == 2)
            {
                if (!CheckIfPasswordLengthValid(args) || !CheckIfPasswordCharsValid(args))
                {
                    return false;
                }
                else
                {
                    return true;
                }                
            }
            else
            {
                return false;
            }
        }
        private static bool CheckIfPasswordCharsValid(string[] args)
        {
            var passwordCharacters = args[1];
            foreach (var character in passwordCharacters) // argumentet skal kun inneholde bokstavene l,L,d eller s
            {
                if (!(character == 'l' || character == 'L' || character == 'd' || character == 's'))
                {
                    return false;
                }
            }
            return true;
        }
        private static bool CheckIfPasswordLengthValid(string[] args)
        {
            var passwordLength = args[0];
            foreach (var character in passwordLength) // lengden skal kun inneholde siffer
            {
                var isDigit = char.IsDigit(character);
                if (!isDigit)
                {
                    return false;
                }
            }
            return true;
        }
        private static void PrintHelpTekst()
        {
            Console.WriteLine(@"PasswordGenerator
            Options:
              - l = lower case letter
              - L = upper case letter
              - d = digit
              - s = special character (!""#¤%&/(){}[]
         Example: PasswordGenerator 14 lLssdd
         Min. 1 lower case
         Min. 1 upper case
         Min. 2 special characters
         Min. 2 digits");
        }
        private static char WriteRandomSpecialCharacter()
        {
            return '(';
        }

        private static char GetRandomUpperCaseLetter()
        {
            return GetRandomLetter('A', 'Z');
        }
        private static char GetRandomLowerCaseLetter()
        {
            return GetRandomLetter('a', 'z');
        }

        private static int GetRandomDigit()
        {
            var randomNum = Random.Next(0, 10);
            return randomNum;
        }

        private static char GetRandomLetter(char min, char max)
        {
            var randomNum = Random.Next(min, max + 1);
            return (char)randomNum;
        }
    }
}
