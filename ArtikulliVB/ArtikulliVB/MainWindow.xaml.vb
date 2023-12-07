Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Class MainWindow
    Dim connection As SqlConnection = ConnectionString.getconnections()

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        LoadData()
    End Sub




    Private Sub changeViewClose() Handles Me.Closed



    End Sub
    'Private Sub addViewsColoses(sender As Object, e As FormClosedEventArgs) Handles addViews.FormClosed



    'End Sub
    Private Sub TipichangeCases(sender As String)

    End Sub

    Private Sub ChangeView_Click(id As Integer)
        Dim changeView As New ChangeView()
        changeView.id = id
        If changeView.ShowDialog() = True Then


        Else
            LoadData()
        End If

    End Sub
    Private Sub AddViews_Click(sender As Object, e As RoutedEventArgs)
        Dim addViews As New AddViews()
        If addViews.ShowDialog() = True Then


        Else
            LoadData()
        End If
    End Sub




    Private Sub changes(sender As Object, e As RoutedEventArgs)

        ChangeView_Click(CType(dataGrid.SelectedItem, DataRowView).Row("ID"))
    End Sub
    Private Sub delete(sender As Object, e As RoutedEventArgs)
        btnDelete_Click(CType(dataGrid.SelectedItem, DataRowView).Row("ID"))
    End Sub













    Private Sub DataGrid_AutoGeneratingColumn(sender As Object, e As DataGridAutoGeneratingColumnEventArgs)
        ' Check if the column is the one you want to modify based on its header or other criteria
        If e.PropertyName = "Lloj" Then
            ' Create a DataGridTemplateColumn
            Dim templateColumn As New DataGridTemplateColumn()
            templateColumn.Header = "Lloj"

            ' Create a DataTemplate with a TextBlock
            Dim dataTemplate As New DataTemplate()
            Dim textBlockFactory As New FrameworkElementFactory(GetType(TextBlock))

            ' Use a converter to convert numeric value to string
            Dim numericToStringConverter As New NumericToStringConverterLloj()

            ' Set the binding with the converter
            Dim binding As New Binding("Lloj")
            binding.Converter = numericToStringConverter
            textBlockFactory.SetBinding(TextBlock.TextProperty, binding)

            ' Set the DataTemplate to the CellTemplate of the template column
            dataTemplate.VisualTree = textBlockFactory
            templateColumn.CellTemplate = dataTemplate

            ' Replace the auto-generated column with the custom template column
            e.Column = templateColumn
        End If
        If e.PropertyName = "Tipi" Then
            ' Create a DataGridTemplateColumn
            Dim templateColumn As New DataGridTemplateColumn()
            templateColumn.Header = "Tipi"

            ' Create a DataTemplate with a TextBlock
            Dim dataTemplate As New DataTemplate()
            Dim textBlockFactory As New FrameworkElementFactory(GetType(TextBlock))

            ' Use a converter to convert numeric value to string
            Dim numericToStringConverter As New NumericToStringConverter()

            ' Set the binding with the converter
            Dim binding As New Binding("Tipi")
            binding.Converter = numericToStringConverter
            textBlockFactory.SetBinding(TextBlock.TextProperty, binding)

            ' Set the DataTemplate to the CellTemplate of the template column
            dataTemplate.VisualTree = textBlockFactory
            templateColumn.CellTemplate = dataTemplate

            ' Replace the auto-generated column with the custom template column
            e.Column = templateColumn
        End If
    End Sub





    Public Class NumericToStringConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            ' Convert numeric value to string based on conditions
            If TypeOf value Is Integer Then
                Dim numericValue As Integer = DirectCast(value, Integer)
                Select Case numericValue
                    Case 0
                        Return "Ushqimore"
                    Case 1
                        Return "Bulmet"
                    Case 2
                        Return "Pije"
                    Case 3
                        Return "Embelsire"
                    Case Else
                        Return value.ToString() ' Fallback to original value as string
                End Select
            Else
                Return value
            End If
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class


    Public Class NumericToStringConverterLloj
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            ' Convert numeric value to string based on conditions
            If TypeOf value Is Integer Then
                Dim numericValue As Integer = DirectCast(value, Integer)
                Select Case numericValue
                    Case 0
                        Return "Importuar"
                    Case 1
                        Return "Vendi"
                    Case Else
                        Return value.ToString() ' Fallback to original value as string
                End Select
            Else
                Return value
            End If
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class








    Private Sub Search_Click(sender As Object, e As RoutedEventArgs)
        Dim searchCriteria As String = txtSearch.Text.Trim()

        If String.IsNullOrEmpty(searchCriteria) Then
            LoadData() ' If search criteria is empty, reload all data
        Else
            Dim adapter As New SqlDataAdapter($"SELECT * FROM cases WHERE Emertimi LIKE '{searchCriteria}%'", connection)
            Dim table As New DataTable()
            adapter.Fill(table)
            dataGrid.ItemsSource = table.DefaultView
        End If
    End Sub











    Private Sub LoadData()
        Dim adapter As New SqlDataAdapter("SELECT * FROM cases", connection)
        Dim table As New DataTable()
        adapter.Fill(table)
        dataGrid.ItemsSource = table.DefaultView

        dataGrid.Columns(6).IsReadOnly = True


        dataGrid.CanUserAddRows = False
    End Sub
    'Private Sub btnInsert_Click(sender As Object, e As RoutedEventArgs)
    '    Dim command As New SqlCommand("INSERT INTO YourTable (Emertimi, Njesia, DataSkadences, Cmimi, Lloj, KaTvsh, Tipi, BarkodKod) VALUES (@Emertimi, @Njesia, @DataSkadences, @Cmimi, @Lloj, @KaTvsh, @Tipi, @BarkodKod)", connection)
    '    command.Parameters.AddWithValue("@Emertimi", txtEmertimi.Text)
    '    command.Parameters.AddWithValue("@Njesia", txtNjesia.Text)
    '    command.Parameters.AddWithValue("@DataSkadences", dpDataSkadences.SelectedDate)
    '    command.Parameters.AddWithValue("@Cmimi", Convert.ToInt32(txtCmimi.Text))
    '    command.Parameters.AddWithValue("@Lloj", If(rbImportuar.IsChecked, "I", "V"))
    '    command.Parameters.AddWithValue("@KaTvsh", If(chkTvsh.IsChecked, 1, 0))
    '    command.Parameters.AddWithValue("@Tipi", cmbTipi.SelectedItem.ToString())
    '    command.Parameters.AddWithValue("@BarkodKod", txtBarkodKod.Text)

    '    connection.Open()
    '    command.ExecuteNonQuery()
    '    connection.Close()

    '    LoadData()
    '    ClearFields()
    'End Sub

    'Private Sub btnUpdate_Click(sender As Object, e As RoutedEventArgs)
    '    Dim command As New SqlCommand("UPDATE YourTable SET Emertimi=@Emertimi, Njesia=@Njesia, DataSkadences=@DataSkadences, Cmimi=@Cmimi, Lloj=@Lloj, KaTvsh=@KaTvsh, Tipi=@Tipi, BarkodKod=@BarkodKod WHERE ID=@ID", connection)
    '    command.Parameters.AddWithValue("@ID", CType(DataGrid.SelectedItem, DataRowView).Row("ID"))
    '    command.Parameters.AddWithValue("@Emertimi", txtEmertimi.Text)
    '    command.Parameters.AddWithValue("@Njesia", txtNjesia.Text)
    '    command.Parameters.AddWithValue("@DataSkadences", dpDataSkadences.SelectedDate)
    '    command.Parameters.AddWithValue("@Cmimi", Convert.ToInt32(txtCmimi.Text))
    '    command.Parameters.AddWithValue("@Lloj", If(rbImportuar.IsChecked, "I", "V"))
    '    command.Parameters.AddWithValue("@KaTvsh", If(chkTvsh.IsChecked, 1, 0))
    '    command.Parameters.AddWithValue("@Tipi", cmbTipi.SelectedItem.ToString())
    '    command.Parameters.AddWithValue("@BarkodKod", txtBarkodKod.Text)

    '    connection.Open()
    '    command.ExecuteNonQuery()
    '    connection.Close()

    '    LoadData()
    '    ClearFields()
    'End Sub

    Private Sub btnDelete_Click(id As Integer)
        Dim command As New SqlCommand("DELETE FROM cases WHERE ID=@ID", connection)
        command.Parameters.AddWithValue("@ID", CType(dataGrid.SelectedItem, DataRowView).Row("ID"))

        connection.Open()
        command.ExecuteNonQuery()
        connection.Close()

        LoadData()
    End Sub



    'Private Sub dataGrid_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
    '    If DataGrid.SelectedItem IsNot Nothing Then
    '        Dim selectedRow As DataRowView = CType(DataGrid.SelectedItem, DataRowView)
    '        txtEmertimi.Text = selectedRow.Row("Emertimi").ToString()
    '        txtNjesia.Text = selectedRow.Row("Njesia").ToString()
    '        dpDataSkadences.SelectedDate = Convert.ToDateTime(selectedRow.Row("DataSkadences"))
    '        txtCmimi.Text = selectedRow.Row("Cmimi").ToString()
    '        rbImportuar.IsChecked = If(selectedRow.Row("Lloj").ToString() = "I", True, False)
    '        chkTvsh.IsChecked = If(selectedRow.Row("KaTvsh").ToString() = "1", True, False)
    '        cmbTipi.SelectedItem = selectedRow.Row("Tipi").ToString()
    '        txtBarkodKod.Text = selectedRow.Row("BarkodKod").ToString()
    '    End If
    'End Sub
End Class

