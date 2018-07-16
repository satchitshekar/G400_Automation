Imports System.IO
Imports G400_Automation.ErrorModel

Public Class ExceptionLog

    Public Sub ExceptionLogTest(logmodel As ErrorModel)

        Dim logFolderPath As String = Directory.GetCurrentDirectory() & "\Logs"
        Dim logFilePath As String = Directory.GetCurrentDirectory() & "\Logs\Log_" & DateTime.Now.ToString("ddMMyyyy") & ".txt"
        Dim logwrite As StreamWriter

        'Create Log Directory
        If Directory.Exists(logFolderPath) = False Then
            Directory.CreateDirectory(logFolderPath)
        End If

        'Create logFile
        If File.Exists(logFilePath) = False Then
            File.Create(logFilePath).Dispose()
        End If

        Dim classname As String = "Error Class : " & logmodel.ClassName.ToString
        Dim methodname As String = "Error Method :" & logmodel.MethodName.ToString
        Dim exceptionMsg As String = "Error Message :" & logmodel.ExceptionMessage.ToString

        ' Write exception message into Log File
        logwrite = My.Computer.FileSystem.OpenTextFileWriter(logFilePath, True)
        logwrite.WriteLine(Environment.NewLine & "----------------------------- Exception Details on " & DateTime.Now.ToString & " -----------------------------")
        logwrite.WriteLine("**************************************************************************************************************")
        logwrite.WriteLine(classname)
        logwrite.WriteLine(methodname)
        logwrite.WriteLine(exceptionMsg)
        logwrite.WriteLine("*************************************************** END ******************************************************")
        logwrite.Close()
        logwrite.Dispose()

        MessageBox.Show("Tool Execution failed due to an error. For further details, please refer the Log file.", "Execution Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Application.Exit()

    End Sub

End Class