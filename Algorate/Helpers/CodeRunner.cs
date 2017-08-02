using Algorate.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Linq;

namespace Algorate.Helpers
{
    public static class CodeRunner
    {
        private static string BasePath { get; set; } = Environment.CurrentDirectory + "\\";
        private static Process Process { get; set; }

        public static Queue<CodeModel> CodeQueue { get; set; }
        public static Dictionary<string, ResultModel> CodeResults { get; private set; }

        // If the queue is being evaluated
        public static bool IsRunning { get; private set; } = false;

        static CodeRunner()
        {
            CodeQueue = new Queue<CodeModel>();
            CodeResults = new Dictionary<string, ResultModel>();
        }

        public static void AddCode(CodeModel cm)
        {
            CodeQueue.Enqueue(cm);
            if (!IsRunning)
            {
                RunThread();
            }
        }

        public static void RunThread()
        {
            var thread = new Thread(RunCodes) { IsBackground = true };
            thread.Start();
        }

        // Deque the oldest test
        // Run it and get the time taken
        // Add the test to the dictionary
        // Repeat
        public static void RunCodes()
        {
            IsRunning = true;
            ConsoleHelper.Thread("Running thread.");
            ConsoleHelper.Info("Using directory: " + BasePath);
            CodeModel codeModel;
            while (CodeQueue.TryDequeue(out codeModel))
            {
                var output = "";

                try
                {
                    // Create the test file
                    SetupFile(codeModel); 

                    // Build test
                    // NEEDS http://go.microsoft.com/fwlink/?LinkId=691126
                    var psi = new ProcessStartInfo(
                        "C:\\Windows\\system32\\cmd.exe"
                    )
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardInput = true,
                        RedirectStandardError = true,
                        Arguments =
                            " /k \"C:\\Program Files (x86)\\Microsoft Visual C++ Build Tools\\vcbuildtools.bat\"" +
                            " amd64"
                    };

                    // Start the process.
                    Process = Process.Start(psi);

                    // Close the build after 2 minutes
                    var tc = new TimerCallback(CloseProcess);
                    var timer = new Timer(tc);
                    timer.Change(120000, Timeout.Infinite);

                    // Get the input stream
                    var istream = Process.StandardInput;

                    istream.WriteLine("cd " + BasePath);
                    istream.WriteLine("cl /EHsc " + "CodeToRun.cpp");
                    istream.WriteLine("CodeToRun.exe");
                    istream.WriteLine("exit");

                    Process.WaitForExit();

                    // process.exitcode
                    // Get the output
                    output = Process.StandardOutput.ReadToEnd();
                    Console.WriteLine(output);
                }
                finally
                {
                    var result = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    float fres = -1;
                    var bres = false;
                    var msg = new List<string>();
                    if (result.Length > 3)
                    {
                        var time = result[result.Length - 3];

                        try
                        {
                            var split = time.Split(' ');
                            fres = float.Parse(split[0]);
                            bres = bool.Parse(split[1]);
                            msg.Add("Code run successfully.");
                        }
                        catch (Exception e)
                        {
                            foreach (var str in result)
                            {
                                if (str.Contains("error")) {
                                    var temp = str;
                                    if (str.Contains("\r"))
                                    {
                                        temp = str.Remove(str.Length - 2, 2);
                                    }
                                    msg.Add(temp);
                                }
                            }
                            ConsoleHelper.Error(e.Message);
                        }
                    }
                    else
                    {
                        fres = -1;
                        bres = false;
                        msg.Add("An unknown error occurred");
                    }
                    try
                    {
                        CodeResults.Add(codeModel.CodeId, new ResultModel(bres, fres, msg));
                    } catch
                    {
                        msg.Add("There is a result with the same ID.");
                    }
                    Process?.Close();
                }
            }
            foreach (var result in CodeResults)
            {
                ConsoleHelper.Info("Code: " + result.Key + " Time: " + result.Value.RunTime);
            }
            ConsoleHelper.Thread("Stopping Thread.");
            IsRunning = false;
        }

        private static void SetupFile(CodeModel cm)
        {
            string code = File.ReadAllText(BasePath + "base.cpp");
            code = code.Replace("// Input", cm.CodeInput);
            code = code.Replace("// Output", cm.ExpectedOutput);
            code = code.Replace("// FunctionCall", cm.Output + " = " + cm.FunctionCall);
            code = code.Replace("// Code", cm.Code);
            code = code.Replace("// Validation", cm.Validator);
            File.WriteAllText(BasePath + "CodeToRun.cpp", code);
        }

        public static void CloseProcess(object obj)
        {
            try
            {
                Process.Kill();
            } catch
            {

            }
        }
    }
}