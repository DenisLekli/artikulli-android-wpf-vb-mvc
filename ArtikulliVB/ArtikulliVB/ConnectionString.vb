Imports System.Data.SqlClient

Public NotInheritable Class ConnectionString
    Public Shared Function getconnections() As SqlConnection
        Dim connectionString As String = "Data Source=DESKTOP-2179KCJ\SQLEXPRESS;Initial Catalog=Artikulli_DB;Integrated Security=True"
        Dim connection As New SqlConnection(connectionString)
        Return connection
    End Function
End Class
