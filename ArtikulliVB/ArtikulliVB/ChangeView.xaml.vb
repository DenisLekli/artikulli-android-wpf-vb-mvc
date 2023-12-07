Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class ChangeView
    Dim connection As SqlConnection = ConnectionString.getconnections()

    Public id As Decimal




    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        PopulateFieldsFromDatabase()
    End Sub
    Private Sub btnInsert_Click(sender As Object, e As RoutedEventArgs)
        If Not ValidateInput() Then
            Return
        End If
        Dim command As New SqlCommand("UPDATE cases SET Emertimi=@Emertimi, Njesia=@Njesia, DataSkadences=@DataSkadences, Cmimi=@Cmimi, Lloj=@Lloj, KaTvsh=@KaTvsh, Tipi=@Tipi, BarkodKod=@BarkodKod WHERE ID=@ID", connection)
        command.Parameters.AddWithValue("@ID", id)
        command.Parameters.AddWithValue("@Emertimi", txtEmertimi.Text)
        command.Parameters.AddWithValue("@Njesia", txtNjesia.Text)
        command.Parameters.AddWithValue("@DataSkadences", dpDataSkadences.SelectedDate)
        command.Parameters.AddWithValue("@Cmimi", Convert.ToInt32(txtCmimi.Text))
        command.Parameters.AddWithValue("@Lloj", If(rbImportuar.IsChecked, 0, 1))
        command.Parameters.AddWithValue("@KaTvsh", If(chkTvsh.IsChecked, 1, 0))
        command.Parameters.AddWithValue("@Tipi", cmbTipi.SelectedIndex)
        command.Parameters.AddWithValue("@BarkodKod", txtBarkodKod.Text)

        connection.Open()
        command.ExecuteNonQuery()
        connection.Close()

        'LoadData()
        'ClearFields()
        Close()
    End Sub
    Private Function ValidateInput() As Boolean
        ' Validate Emertimi (Product Name)
        If String.IsNullOrWhiteSpace(txtEmertimi.Text) Then
            MessageBox.Show("Please enter a valid product name.")
            Return False
        End If

        ' Validate Njesia (Unit)
        If String.IsNullOrWhiteSpace(txtNjesia.Text) Then
            MessageBox.Show("Please enter a valid unit.")
            Return False
        End If

        ' Validate DataSkadences (Expiration Date)
        If dpDataSkadences.SelectedDate Is Nothing OrElse dpDataSkadences.SelectedDate < DateTime.Now Then
            MessageBox.Show("Please enter a valid expiration date.")
            Return False
        End If

        ' Validate Cmimi (Price)
        Dim cmimi As Integer
        If Not Integer.TryParse(txtCmimi.Text, cmimi) OrElse cmimi <= 0 Then
            MessageBox.Show("Please enter a valid positive price.")
            Return False
        End If

        ' Validate Lloj (Type)
        If rbImportuar.IsChecked Is Nothing AndAlso rbVendi.IsChecked Is Nothing Then
            MessageBox.Show("Please select a type (Importuar or Vendi).")
            Return False
        End If

        ' Validate KaTvsh (Has VAT)
        If chkTvsh.IsChecked Is Nothing Then
            MessageBox.Show("Please indicate whether the product has VAT.")
            Return False
        End If

        ' Validate Tipi (Category)
        If cmbTipi.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a category.")
            Return False
        End If

        ' Validate BarkodKod (Barcode)
        If String.IsNullOrWhiteSpace(txtBarkodKod.Text) Then
            MessageBox.Show("Please enter a valid barcode.")
            Return False
        End If
        Return True
    End Function

    Private Sub PopulateFieldsFromDatabase()
        Try
            connection.Open()


            Dim query As String = "SELECT Emertimi, Njesia, DataSkadences, Cmimi, Lloj, KaTvsh, Tipi, BarkodKod FROM cases WHERE id=" + id.ToString()
            Using command As New SqlCommand(query, connection)
                Dim reader As SqlDataReader = command.ExecuteReader()

                If reader.Read() Then
                    ' Populate the fields from the data reader
                    txtEmertimi.Text = reader("Emertimi").ToString()
                    txtNjesia.Text = reader("Njesia").ToString()

                    ' Check for DBNull before assigning to DateTime
                    If Not reader.IsDBNull(reader.GetOrdinal("DataSkadences")) Then
                        dpDataSkadences.SelectedDate = reader.GetDateTime(reader.GetOrdinal("DataSkadences"))
                    End If

                    ' Continue similarly for other fields
                    txtCmimi.Text = reader("Cmimi").ToString()
                    ' You can also set values for radio buttons, check boxes, and combo boxes

                    chkTvsh.IsChecked = Convert.ToBoolean(reader("KaTvsh"))
                    cmbTipi.SelectedIndex = Convert.ToInt32(reader("Tipi"))
                    rbImportuar.IsChecked = (Convert.ToInt32(reader("Lloj")) = 0)
                    rbVendi.IsChecked = (Convert.ToInt32(reader("Lloj")) = 1)
                    txtBarkodKod.Text = reader("BarkodKod").ToString()
                Else
                    MessageBox.Show("No data found.")
                End If
                connection.Close()
            End Using
        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}")
            connection.Close()
        End Try
    End Sub
End Class
