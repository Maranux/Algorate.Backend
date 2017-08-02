using System.Collections.Generic;

namespace Algorate.Models
{
    public class CodeModel
    {
        // An ID to find the code.
        public string CodeId { get; set; }
        // The code of any function(s) to add to the file. This is what is tested.
        public string Code { get; set; }
        // The input data.
        public string CodeInput { get; set; }
        // The expected output data.
        public string ExpectedOutput { get; set; }
        // The variable to hold the output of the function. EX: string[] output
        public string Output { get; set; }
        // The actual function call. EX: CodeToRun(inputName);
        public string FunctionCall { get; set; }
        public string Validator { get; set; }
        public List<string> Includes { get; set; }
        public bool Validate()
        {
            if (CodeId == null || Code == null || CodeInput == null || ExpectedOutput == null || Includes == null || FunctionCall == null)
            {
                return false;
            }
            return true;
        }
    }
}