Imports System.Data.OleDb
Imports G400_Automation.GlobalVariables
Public Class MainClass

    Public Sub Main()

        Dim ExceptionClass As ExceptionLog = New ExceptionLog

        Try
            Dim ole As New OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;Data Source=" & TARFilePath & " ;Extended Properties=Excel 12.0 Xml;")
            ole.Open()
            Dim U_Report = New OleDbCommand("Insert into [Sheet5$]  ([Credit Card No]) values (10)", ole)
            U_Report.ExecuteNonQuery()
            ole.Close()
            MessageBox.Show("Update Complete")

        Catch ex As Exception
            Dim eModel As ErrorModel = New ErrorModel
            eModel.ClassName = "MainClass"
            eModel.MethodName = "Main"
            eModel.ExceptionMessage = ex.ToString
            ExceptionClass.ExceptionLogTest(eModel)
        End Try


    End Sub

End Class
