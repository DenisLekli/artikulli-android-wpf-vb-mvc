Imports System.Data.SqlClient

Public Class AddViews
    Dim connection As SqlConnection = ConnectionString.getconnections()

    Private Sub btnInsert_Click(sender As Object, e As RoutedEventArgs)

        If Not ValidateInput() Then
            Return
        End If

        Dim command As New SqlCommand("INSERT INTO cases (Emertimi, Njesia, DataSkadences, Cmimi, Lloj, KaTvsh, Tipi, BarkodKod) VALUES (@Emertimi, @Njesia, @DataSkadences, @Cmimi, @Lloj, @KaTvsh, @Tipi, @BarkodKod)", connection)
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
End Class
