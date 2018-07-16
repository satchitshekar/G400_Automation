Imports IBMISeries
Imports G400_Automation.GlobalVariables
Public Class AutomateG400

    Dim Session_ID As Char

    Public Sub G400Automation()

        Dim ExceptionClass As ExceptionLog = New ExceptionLog

        Try
            SignOn()
        Catch ex As Exception
            Dim eModel As ErrorModel = New ErrorModel
            eModel.ClassName = "AutomateG400"
            eModel.MethodName = "G400Automation"
            eModel.ExceptionMessage = ex.ToString
            ExceptionClass.ExceptionLogTest(eModel)
        End Try


    End Sub

    '*****************************************
    'Func Name = SignOn
    'Description = Will Sign into G400 System
    '*****************************************
    Public Function SignOn()

        Dim G400Process As Process
        Dim WindowTitle As String

        G400Process = Process.Start(G400SystemPath)
        G400Process.WaitForExit(5000)

        MessageBox.Show("Process Exit")

        WindowTitle = G400Process.MainWindowTitle

        'Connect to the Presentation Space
        Wrapper.HLL_QuerySession()
        Session_ID = Trim(Strings.Left(Wrapper.mstr_QueryData, 1))
        MessageBox.Show(Session_ID)
        Wrapper.HLL_ConnectPS(Session_ID)

        AutoIt.AutoItX.AutoItSetOption("WinTitleMatchMode", 2)
        AutoIt.AutoItX.ControlSetText("IBM i signon", "", "Edit2", G400_Username)
        AutoIt.AutoItX.ControlSetText("IBM i signon", "", "Edit3", G400_Password)
        AutoIt.AutoItX.ControlSend("IBM i signon", "", "Button1", "{ENTER}")

    End Function

End Class
