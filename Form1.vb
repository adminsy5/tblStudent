Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles
Imports System.Diagnostics
Public Class Form1
    Dim conStr As String = "Data Source=MPIYUSH3510-AMD;Initial Catalog=Admin574;Integrated Security=true"
    Dim sqlCmd As SqlCommand
    Dim Connection As SqlConnection
    Dim dtAdapater As SqlDataAdapter
    Dim dtSet As DataSet
    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        Connection = New SqlConnection("Data Source=MPIYUSH3510-AMD;Initial Catalog=Admin574;Integrated Security=true")
        Try
            Connection.Open()
            If Connection.State = ConnectionState.Open Then
                ' MsgBox("Connected To Student Database !")
            End If
        Catch ec As Exception
            MsgBox(ec.Message)
        End Try

        Try
            If TextBoxRno.Text = "" And TextBoxName.Text = "" And ComboBoxGender.Text = "" And TextBoxSid.Text = "" And TextBoxAndroid.Text = "" And TextBoxJava.Text = "" And TextBoxDotnet.Text = "" And TextBoxIot.Text = "" And TextBoxIs.Text = "" Then
                MsgBox("All Fields Are Empty !")
                Return
            End If

            If TextBoxRno.Text = "" Then
                MsgBox("Please Enter Roll Number !")
                Return
            End If

            If TextBoxName.Text = "" Then
                MsgBox("Name can't be empty !")
                Return
            End If

            If ComboBoxGender.Text = "" Then
                MsgBox("Please select Gender correctly !")
                Return
            End If

            If TextBoxSid.Text = "" Then
                MsgBox(" Sid can't be empty  !")
                Return
            End If

            If TextBoxAndroid.Text = "" Then
                MsgBox("subject android Can't empty !")
                Return
            End If

            If TextBoxJava.Text = "" Then
                MsgBox("subject Java Can't empty !")
                Return
            End If

            If TextBoxDotnet.Text = "" Then
                MsgBox("subject .Net Can't empty !")
                Return
            End If

            If TextBoxIot.Text = "" Then
                MsgBox("subject Iot Can't empty !")
                Return
            End If

            If TextBoxIs.Text = "" Then
                MsgBox("subject Is Can't empty !")
                Return
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        CreatetblStudent()
        InsertIntotblStudent()
        ShowDataFromtblStudent()
    End Sub

    Public Sub CreatetblStudent()
        Try
            sqlCmd = New SqlCommand("Create table tblStudent(sid int unique,name varchar(40),rno int primary key,gender varchar(10),android int,java int ,dotnet int,iot int,is1 int,per float)", Connection)
            sqlCmd.ExecuteNonQuery()
            MsgBox("Table Created !")
        Catch ec As Exception
            MsgBox(ec.Message)
        End Try
    End Sub

    Public Sub InsertIntotblStudent()
        Try
            Dim sid As Integer = Convert.ToInt32(TextBoxSid.Text)
            Dim name As String = TextBoxName.Text
            Dim rno As Integer = Convert.ToInt32(TextBoxRno.Text)
            Dim gender As String = ComboBoxGender.Text
            Dim android As Integer = Convert.ToInt32(TextBoxAndroid.Text)
            Dim java As Integer = Convert.ToInt32(TextBoxJava.Text)
            Dim dotnet As Integer = Convert.ToInt32(TextBoxDotnet.Text)
            Dim iot As Integer = Convert.ToInt32(TextBoxIot.Text)
            Dim is1 As Integer = Convert.ToInt32(TextBoxIs.Text)


            sqlCmd = New SqlCommand("insert into tblStudent(sid,name,rno,gender,android,java,dotnet,iot,is1) values(@sid,@name,@rno,@gender,@android,@java,@dotnet,@iot,@is1)", Connection)
            sqlCmd.Parameters.AddWithValue("@sid", sid)
            sqlCmd.Parameters.AddWithValue("@name", name)
            sqlCmd.Parameters.AddWithValue("@rno", rno)
            sqlCmd.Parameters.AddWithValue("@gender", gender)
            sqlCmd.Parameters.AddWithValue("@android", android)
            sqlCmd.Parameters.AddWithValue("@java", java)
            sqlCmd.Parameters.AddWithValue("@dotnet", dotnet)
            sqlCmd.Parameters.AddWithValue("@iot", iot)
            sqlCmd.Parameters.AddWithValue("@is1", is1)
            sqlCmd.ExecuteNonQuery()
            MsgBox("Inserted !")
        Catch ec As Exception
            MsgBox(ec.Message)
        End Try

        Try
            sqlCmd = New SqlCommand("update tblstudent set per=(android+java+iot+is1+dotnet)/5", Connection)
            sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub ShowDataFromtblStudent()
        Try
            dtAdapater = New SqlDataAdapter("select * from tblStudent order by rno", Connection)
            dtSet = New DataSet()
            dtAdapater.Fill(dtSet, "tblStudent")
            DataGridViewReader.DataSource = dtSet.Tables(0).DefaultView
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ButtonRefresh_Click(sender As Object, e As EventArgs) Handles ButtonRefresh.Click
        Connection = New SqlConnection(conStr)
        Connection.Open()
        Try
            dtAdapater = New SqlDataAdapter("select * from tblStudent order by rno", Connection)
            dtSet = New DataSet()
            dtAdapater.Fill(dtSet, "tblStudent")
            DataGridViewReader.DataSource = dtSet.Tables(0).DefaultView
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim str As String = "https://mpiyush3510.carrd.co/"
        Diagnostics.Process.Start("C:\Program Files\Google\Chrome\Application\chrome.exe", str)
    End Sub

    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        Connection = New SqlConnection("Data Source=MPIYUSH3510-AMD;Initial Catalog=Admin574;Integrated Security=true")
        Connection.Open()

        If DataGridViewReader.RowCount = 0 Then
            MsgBox("unable to deleted, table is empty !", MsgBoxStyle.Critical, "failed")
            Return
        End If

        If MsgBox("Delete record ?", MsgBoxStyle.Question + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Cancel Then Return

        Try
            If DataGridViewReader.AreAllCellsSelected(0) = True Then
                sqlCmd = New SqlCommand("delete from tblStudent", Connection)
                sqlCmd.ExecuteNonQuery()
                ButtonRefresh_Click(sender, e)
            End If
        Catch ec As Exception
            MsgBox(ec.Message)
        End Try

        Try
            For Each row As DataGridViewRow In DataGridViewReader.SelectedRows
                If row.Selected Then
                    sqlCmd = New SqlCommand("delete from tblStudent where sid = '" & row.DataBoundItem(0).ToString & "'", Connection)
                    sqlCmd.ExecuteNonQuery()
                    ButtonRefresh_Click(sender, e)
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ButtonClear_Click(sender As Object, e As EventArgs) Handles ButtonClear.Click
        TextBoxRno.Clear()
        TextBoxName.Clear()
        ComboBoxGender.SelectedItem = "- Choose Gender -"
        TextBoxSid.Clear()
        TextBoxAndroid.Clear()
        TextBoxJava.Clear()
        TextBoxDotnet.Clear()
        TextBoxIot.Clear()
        TextBoxIs.Clear()
    End Sub

    Private Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click
        If TextBoxRno.Text = "" And TextBoxName.Text = "" And ComboBoxGender.Text = "" And TextBoxSid.Text = "" And TextBoxAndroid.Text = "" And TextBoxJava.Text = "" And TextBoxDotnet.Text = "" And TextBoxIot.Text = "" And TextBoxIs.Text = "" Then
            MsgBox("All Fields Are Empty !")
            Return
        End If

        If TextBoxRno.Text = "" Then
            MsgBox("Please Enter Roll Number !")
            Return
        End If

        If TextBoxName.Text = "" Then
            MsgBox("Name can't be empty !")
            Return
        End If

        If ComboBoxGender.Text = "" Then
            MsgBox("Please select Gender correctly !")
            Return
        End If

        If TextBoxSid.Text = "" Then
            MsgBox(" Sid can't be empty  !")
            Return
        End If

        If TextBoxAndroid.Text = "" Then
            MsgBox("subject android Can't empty !")
            Return
        End If

        If TextBoxJava.Text = "" Then
            MsgBox("subject Java Can't empty !")
            Return
        End If

        If TextBoxDotnet.Text = "" Then
            MsgBox("subject .Net Can't empty !")
            Return
        End If

        If TextBoxIot.Text = "" Then
            MsgBox("subject Iot Can't empty !")
            Return
        End If

        If TextBoxIs.Text = "" Then
            MsgBox("subject Is Can't empty !")
            Return
        End If

        Try
            Dim sid As Integer = Convert.ToInt32(TextBoxSid.Text)
            Dim name As String = TextBoxName.Text
            Dim rno As Integer = Convert.ToInt32(TextBoxRno.Text)
            Dim gender As String = ComboBoxGender.Text
            Dim android As Integer = Convert.ToInt32(TextBoxAndroid.Text)
            Dim java As Integer = Convert.ToInt32(TextBoxJava.Text)
            Dim dotnet As Integer = Convert.ToInt32(TextBoxDotnet.Text)
            Dim iot As Integer = Convert.ToInt32(TextBoxIot.Text)
            Dim is1 As Integer = Convert.ToInt32(TextBoxIs.Text)


            sqlCmd = New SqlCommand("Update tblStudent set name=@name,sid=@sid,gender=@gender,android=@android,java=@java,dotnet=@dotnet,iot=@iot,is1=@is1 where rno=@rno", Connection)
            sqlCmd.Parameters.AddWithValue("@sid", sid)
            sqlCmd.Parameters.AddWithValue("@name", name)
            sqlCmd.Parameters.AddWithValue("@rno", rno)
            sqlCmd.Parameters.AddWithValue("@gender", gender)
            sqlCmd.Parameters.AddWithValue("@android", android)
            sqlCmd.Parameters.AddWithValue("@java", java)
            sqlCmd.Parameters.AddWithValue("@dotnet", dotnet)
            sqlCmd.Parameters.AddWithValue("@iot", iot)
            sqlCmd.Parameters.AddWithValue("@is1", is1)
            sqlCmd.ExecuteNonQuery()
            MsgBox("Updated !")
            ButtonRefresh_Click(sender, e)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CheckBoxCondition_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxCondition.CheckedChanged
        Connection = New SqlConnection(conStr)
        Connection.Open()
        Try
            dtAdapater = New SqlDataAdapter("select * from tblStudent where per>75 ", Connection)
            dtSet = New DataSet()
            dtAdapater.Fill(dtSet, "tblStudent")
            DataGridViewReader.DataSource = dtSet.Tables(0).DefaultView
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ButtonSidGen_Click(sender As Object, e As EventArgs) Handles ButtonSidGen.Click
        Dim random As Random = New Random
        Dim num As Integer
        num = (random.Next(1, 9999))
        Dim sidGen As String = Strings.Right("0000" & num.ToString(), 4)
        TextBoxSid.Text = sidGen

        Try
            Connection = New SqlConnection(conStr)
            Connection.Open()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        Try
            sqlCmd = New SqlCommand("select * from tblStudent where sid = '" & TextBoxSid.Text & "'", Connection)
            sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'If dtSet.Row.Count > 0 Then
        '    ButtonSidGen_Click(sender, e)
        'End If
    End Sub
End Class
