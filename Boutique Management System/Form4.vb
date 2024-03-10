Imports MySql.Data.MySqlClient

Public Class Form4
    ' Replace with your actual connection string
    Private connectionString As String = "Server=localhost;Database=butique;User=root;Password=admin;"

    Private Sub Guna2GradientButton14_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton14.Click
        LoadDataToDataGridView()
    End Sub

    Private Sub LoadDataToDataGridView()
        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM sales"
                Using cmd As New MySqlCommand(query, connection)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)

                    Guna2DataGridView1.DataSource = Nothing ' Clear the current DataSource
                    Guna2DataGridView1.DataSource = dt ' Set the DataSource to the new DataTable
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error loading sales data: {ex.Message}")
        End Try
    End Sub
End Class
