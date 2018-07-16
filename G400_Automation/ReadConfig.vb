Imports System.Data.OleDb
Imports G400_Automation.GlobalVariables
Imports System.IO

Public Class ReadConfig
    Public Sub configData()


        Dim expClass As ExceptionLog = New ExceptionLog
        Dim ResourcePath As String = Path.GetFullPath(Application.StartupPath & "\Resources")

        Try
            Dim ole As New OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;Data Source= " & ResourcePath & "\Config.xlsx ;Extended Properties=Excel 12.0 Xml;")
            ole.Open()
            Dim ConfData = New OleDbDataAdapter("Select [Username],[Password] from [UserCredentials_G400$]", ole)
            Dim Dt As New DataSet
            ConfData.Fill(Dt)

            G400_Username = Trim(Dt.Tables(0).Rows(0).ItemArray(0).ToString)
            G400_Password = Trim(Dt.Tables(0).Rows(0).ItemArray(1).ToString)

            ole.Close()

        Catch ex As Exception
            Dim eModel As ErrorModel = New ErrorModel
            eModel.ClassName = "MainClass"
            eModel.MethodName = "Main"
            eModel.ExceptionMessage = ex.ToString
            expClass.ExceptionLogTest(eModel)
        End Try
    End Sub


End Class
