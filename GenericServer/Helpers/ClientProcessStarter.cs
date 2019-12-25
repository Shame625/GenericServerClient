using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GenericServer.Helpers
{
    public static class ClientProcessStarter
    {
        public static void StartProcesses(int count)
        {
            for (int i = 0; i < count; i++)
            {
                try
                {
                    using (Process myProcess = new Process())
                    {
                        myProcess.StartInfo.UseShellExecute = true;
                        myProcess.StartInfo.FileName = "D:\\GenericServerClient\\GenericClient\\bin\\Debug\\netcoreapp3.0\\GenericClient.exe";
                        myProcess.StartInfo.CreateNoWindow = false;
                        myProcess.Start();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
