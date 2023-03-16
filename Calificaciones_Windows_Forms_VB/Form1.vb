Imports System.IO

Public Class Form1
    'hago uso de objetos o clases de tipos que Me ayudaran a manejar archivos .bin y escoger rutas para guardar y leer
    'y los globalizo
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
        'al cargar el programa damos la opcion de leer un archivo creado anteriormente
        ofd = New OpenFileDialog()
        ofd.Title = "Calificaciones del alumno"
        ofd.Filter = "Archivos binarios (*.bin)|*.bin"
        'doy la opcion al usuario de cargar otro documento
        Dim opc As DialogResult = MessageBox.Show("Quieres usar un archivo existente?", "???", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If opc = DialogResult.Yes Then

            If ofd.ShowDialog() = DialogResult.OK Then

                'por lo que vi al investigar sobre el manejo de archivos binarios cuido
                'que tanto la lectura como escritura del archivo sean en el mismo formato o  tipo de daaato para evitar errores
                fileStream = New FileStream(ofd.FileName, FileMode.Open, FileAccess.Read)
                reader = New BinaryReader(fileStream)

                While reader.BaseStream.Position < reader.BaseStream.Length

                    'tanto la manera en como se escribe como la de cuando se lee tiene tanto que ver en los archivos bin
                    'que el mas minimo error podria arruinar una estructura de datos pequeña como la de este proyecto
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

        'haciendo uso de eventos indico qe al momento de que se este cerrando el programa el usuario
        'tenga la oportunidad de guardar archivos
        Dim x As DialogResult = MessageBox.Show("Deseas guardar las calificaciones?", "???", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If x = DialogResult.Yes Then
            Dim ruta As SaveFileDialog = New SaveFileDialog()
            ruta.Filter = "Archivos binarios (*.bin)|*.bin"
            ruta.Title = "Calificaciones del alumno"

            'detecto el error y lo evito antes de que suceda
            If ruta.ShowDialog() = DialogResult.Cancel Then
                MessageBox.Show("Error al escoger la ruta de archivo. ", "Error", MessageBoxButtons.OK)
                Return
            End If

            'con el filestream puedo escoger que clase de cosa quiero hacer en caunto de archivos se trata
            'en este caso lo unico que quiero hacer es crear un archivo en base a la ruta que me dieron anteriormente
            'y escrbir sobre el en este punto del programa
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
        'en este apartado simplemente registro los datos entregados por el usuario en el dgv
        dgvCalificaciones.Rows.Add(nud1.Value.ToString(), nud2.Value.ToString(), nud3.Value.ToString(), nud4.Value.ToString(), nud5.Value.ToString(), nud6.Value.ToString())
        btnEnviar.Enabled = False
    End Sub
End Class
