Imports IBMISeries
Imports G400_Automation.GlobalVariables
Imports System.Threading
Public Class AutomateG400

    Dim Session_ID As Char
    Dim ExceptionClass As ExceptionLog = New ExceptionLog

    Public Sub G400Automation()
        Try
            SignOn()
            NavHomeScrn()
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
    'Description = Will Login to G400 System
    '*****************************************
    Public Function SignOn()

        Dim G400Process As Process
        Dim WindowTitle As String
        Dim sign_str As String
        Dim rls_counter As Integer = 0

        G400Process = Process.Start(G400SystemPath)
        G400Process.WaitForExit(5000)

        WindowTitle = G400Process.MainWindowTitle

        'Connect to the Presentation Space
        Wrapper.HLL_QuerySession()
        Session_ID = Trim(Strings.Left(Wrapper.mstr_QueryData, 1))
        Wrapper.HLL_ConnectPS(Session_ID)

        AutoIt.AutoItX.AutoItSetOption("WinTitleMatchMode", 2)
        AutoIt.AutoItX.ControlSetText("IBM i signon", "", "Edit2", G400_Username)
        AutoIt.AutoItX.ControlSetText("IBM i signon", "", "Edit3", G400_Password)
        AutoIt.AutoItX.ControlSend("IBM i signon", "", "Button1", "{ENTER}")

        'Read the Sign On Screen and Enter the login credentials
        Do
            Thread.Sleep(1000)
            sign_str = Nothing
            Wrapper.HLL_ReadScreen(Wrapper.getPos(1, 36), 7, sign_str)
            sign_str = Trim(Strings.Left(sign_str, 7))
            rls_counter = rls_counter + 1

            If sign_str = "Sign On" Then
                Exit Do
            ElseIf rls_counter = 10 Then
                Dim eModel As ErrorModel = New ErrorModel
                eModel.ClassName = "AutomateG400"
                eModel.MethodName = "SignOn"
                eModel.ExceptionMessage = "Tool Could not Login to G400 System. Please check for Access and Try Again Later"
                ExceptionClass.ExceptionLogTest(eModel)
            End If
        Loop Until (sign_str = "Sign On")

        Wrapper.HLL_CopyStringToPS(G400_Username, Wrapper.getPos(6, 53))
        Wrapper.HLL_CopyStringToPS(G400_Password, Wrapper.getPos(7, 53))
        Wrapper.SendStr("@E")
        Wrapper.HLL_Wait()

    End Function

    '*****************************************************
    'Func Name = NavHomeScrn
    'Description = Navigate to Home Screen of G400 System
    '****************************************************
    Public Function NavHomeScrn()

        Dim MainScrnName As String

        Do
            Wrapper.SendStr("@E")
            Wrapper.HLL_Wait()

            MainScrnName = Nothing
            Wrapper.HLL_ReadScreen(Wrapper.getPos(1, 31), 18, MainScrnName)
            MainScrnName = Trim(Strings.Left(MainScrnName, 18))

            MessageBox.Show(MainScrnName)

        Loop Until (MainScrnName = "System Master Menu")

    End Function

End Class
