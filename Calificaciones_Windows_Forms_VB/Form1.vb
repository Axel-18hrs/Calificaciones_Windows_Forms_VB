Imports System.IO

Public Class Form1
    Private reader As BinaryReader
    Private writer As BinaryWriter
    Private readerStream As StreamReader
    Private fileStream As FileStream
    Private ofd As OpenFileDialog

    Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ofd = New OpenFileDialog()
        ofd.Title = "Calificaciones del alumno"
        ofd.Filter = "Archivos binarios (*.bin)|*.bin"

        Dim opc As DialogResult = MessageBox.Show("Quieres usar un archivo existente?", "???", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If opc = DialogResult.Yes Then

            If ofd.ShowDialog() = DialogResult.OK Then

                fileStream = New FileStream(ofd.FileName, FileMode.Open, FileAccess.Read)
                reader = New BinaryReader(fileStream)

                While reader.BaseStream.Position < reader.BaseStream.Length
                    Dim value1 As Double = reader.ReadDouble()
                    Dim value2 As Double = reader.ReadDouble()
                    Dim value3 As Double = reader.ReadDouble()
                    Dim value4 As Double = reader.ReadDouble()
                    Dim value5 As Double = reader.ReadDouble()
                    Dim value6 As Double = reader.ReadDouble()
                    dgvCalificaciones.Rows.Add(value1, value2, value3, value4, value5, value6)
                    btnEnviar.Enabled = False
                    MessageBox.Show("No se podra calificar nuevamente si ya se establecio un archivo para este programa", "???", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End While
                reader.Close()
            Else
                MessageBox.Show("Error al guardar el archivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            End If
        End If
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Dim x As DialogResult = MessageBox.Show("Deseas guardar las calificaciones?", "???", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If x = DialogResult.Yes Then
            Dim ruta As SaveFileDialog = New SaveFileDialog()
            ruta.Filter = "Archivos binarios (*.bin)|*.bin"
            ruta.Title = "Calificaciones del alumno"

            If ruta.ShowDialog() = DialogResult.Cancel Then
                MessageBox.Show("Error al escoger la ruta de archivo. ", "Error", MessageBoxButtons.OK)
                Return
            End If
            Dim rutad As FileStream = New FileStream(ruta.FileName, FileMode.Create, FileAccess.Write)
            writer = New BinaryWriter(rutad)
            For Each row As DataGridViewRow In dgvCalificaciones.Rows
                ' Itera sobre cada celda en la fila
                For Each cell As DataGridViewCell In row.Cells
                    ' Escribe el valor de la celda en el archivo binario
                    writer.Write(Convert.ToDouble(cell.Value))
                Next
            Next

            writer.Close()
            rutad.Close()
        End If
    End Sub

    Private Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        dgvCalificaciones.Rows.Add(nud1.Value.ToString(), nud2.Value.ToString(), nud3.Value.ToString(), nud4.Value.ToString(), nud5.Value.ToString(), nud6.Value.ToString())
        btnEnviar.Enabled = False
    End Sub
End Class
