using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace PracticaFinal
{
    public partial class Formulario : Form
    {
        #region Atributo solo lectura para conexión con BBDD
        // Cadena de conexión a la base de datos
        private readonly string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=|DataDirectory|\practica.accdb";
        #endregion

        #region Constructor
        public Formulario()
        {
            InitializeComponent();
            MostrarFormularioCrearAlumno(); // Mostrar formulario de crear alumno al iniciar
            this.Resize += new EventHandler(Formulario_Resize); // Evento para actualizar el tamaño cuando se redimensione la ventana
        }
        #endregion

        #region Conexión con BBDD
        // Método para obtener una conexión a la base de datos
        private OleDbConnection ObtenerConexion()
        {
            OleDbConnection conn = new OleDbConnection(connectionString); // Crear conexión
            conn.Open(); // Abrir conexión
            return conn; // Devolver conexión abierta
        }
        #endregion

        #region Crear Alumno
        // Método para mostrar el formulario de creación de alumno
        private void MenuCrearAlumno_Click(object sender, EventArgs e)
        {
            MostrarFormularioCrearAlumno(); // Llamar al método para mostrar el formulario de crear alumno
        }

        // Método que muestra el formulario para crear un nuevo alumno
        #region Crear Alumno
private void MostrarFormularioCrearAlumno()
{
    panel.Controls.Clear(); // Limpiar el panel antes de agregar nuevos controles.
    panel.Padding = new Padding(20); // Margen interior del panel.
    panel.BackColor = Color.White; // Opcional: Define el color de fondo.

    // Título centrado arriba
    Label lblTitulo = new Label()
    {
        Text = "Crear Alumno",
        TextAlign = ContentAlignment.MiddleCenter,
        Font = new Font("Arial", 18, FontStyle.Bold),
        Dock = DockStyle.Top,
    };
    panel.Controls.Add(lblTitulo);

    // TableLayoutPanel central para los controles
    TableLayoutPanel tableLayout = new TableLayoutPanel()
    {
        RowCount = 4,
        ColumnCount = 2,
        AutoSize = true,
        Anchor = AnchorStyles.None, // Evita que se estire innecesariamente
        Dock = DockStyle.None,
        BackColor = Color.Transparent
    };

    tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F)); // Columnas
    tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));

    tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // Filas
    tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
    tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
    tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

    // Controles en el centro
    Label lblNombre = new Label() 
    { 
        Text = "Nombre:", 
        TextAlign = ContentAlignment.MiddleRight, 
        Anchor = AnchorStyles.Right | AnchorStyles.Top, // Ajusta hacia arriba
        Padding = new Padding(0, 5, 0, 0) // Espacio superior
    };
    TextBox txtNombre = new TextBox() 
    { 
        Dock = DockStyle.Fill, 
        Margin = new Padding(3, 5, 3, 3) // Espaciado superior del TextBox
    };

    Label lblApellidos = new Label() 
    { 
        Text = "Apellidos:", 
        TextAlign = ContentAlignment.MiddleRight, 
        Anchor = AnchorStyles.Right | AnchorStyles.Top, 
        Padding = new Padding(0, 5, 0, 0) 
    };
    TextBox txtApellidos = new TextBox() 
    { 
        Dock = DockStyle.Fill, 
        Margin = new Padding(3, 5, 3, 3) 
    };

    Label lblNif = new Label() 
    { 
        Text = "NIF:", 
        TextAlign = ContentAlignment.MiddleRight, 
        Anchor = AnchorStyles.Right | AnchorStyles.Top, 
        Padding = new Padding(0, 5, 0, 0) 
    };
    TextBox txtNif = new TextBox() 
    { 
        Dock = DockStyle.Fill, 
        Margin = new Padding(3, 5, 3, 3) 
    };

    CheckBox chkBaja = new CheckBox() 
    { 
        Text = "Baja", 
        Anchor = AnchorStyles.Left | AnchorStyles.Top 
    };

    // Agregar controles a la tabla
    tableLayout.Controls.Add(lblNombre, 0, 0);
    tableLayout.Controls.Add(txtNombre, 1, 0);

    tableLayout.Controls.Add(lblApellidos, 0, 1);
    tableLayout.Controls.Add(txtApellidos, 1, 1);

    tableLayout.Controls.Add(lblNif, 0, 2);
    tableLayout.Controls.Add(txtNif, 1, 2);

    tableLayout.Controls.Add(new Label() { Dock = DockStyle.Fill }, 0, 3); // Espacio vacío
    tableLayout.Controls.Add(chkBaja, 1, 3);

    // Centrar la tabla en el panel
    tableLayout.Location = new Point(
        (panel.Width - tableLayout.PreferredSize.Width) / 2 - 25,
        lblTitulo.Bottom + 30
    );

    tableLayout.Anchor = AnchorStyles.None;
    panel.Controls.Add(tableLayout);

    // Botón Crear centrado abajo
    Button btnGuardar = new Button()
    {
        Text = "Crear",
        Width = 150,
        Height = 40,
        BackColor = Color.Black,
        ForeColor = Color.White,
        Dock = DockStyle.Bottom,
        Margin = new Padding(10),
        Anchor = AnchorStyles.None
    };
    btnGuardar.Click += (s, e) => CrearAlumno(txtNombre, txtApellidos, txtNif, chkBaja);

    panel.Controls.Add(btnGuardar);
    btnGuardar.Left = (panel.Width - btnGuardar.Width) / 2; // Centramos el botón
    btnGuardar.Top = tableLayout.Bottom + 10; // Botón debajo del grid
}

        #endregion


        // Método para crear un nuevo alumno en la base de datos
        private void CrearAlumno(TextBox txtNombre, TextBox txtApellidos, TextBox txtNif, CheckBox chkBaja)
        {
            if (ValoresCorrectos(txtNif.Text, txtNombre.Text, txtApellidos.Text)) // Validar si los valores de los campos son los esperados
            {
                if (AlumnoExiste(txtNif.Text))
                {
                    MessageBox.Show("Alumno dado de alta exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarValores(txtNombre, txtApellidos, txtNif, chkBaja); // Limpiar los TextBox y Checkbox
                    return ;
                }
                using (OleDbConnection conn = ObtenerConexion()) // Establecer conexión con la base de datos
                {
                    string query = "INSERT INTO Alumnos (Nif, Nombre, Apellidos, Baja) VALUES (@Nif, @Nombre, @Apellidos, @Baja)";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn)) // Crear comando de inserción
                    {
                        cmd.Parameters.AddWithValue("@Nif", txtNif.Text.ToString()); // Agregar el parámetro NIF
                        cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim().ToString()); // Agregar el parámetro Nombre
                        cmd.Parameters.AddWithValue("@Apellidos", txtApellidos.Text.Trim().ToString()); // Agregar el parámetro Apellidos
                        cmd.Parameters.AddWithValue("@Baja", chkBaja.Checked); // Agregar el parámetro Baja

                        cmd.ExecuteNonQuery(); // Ejecutar la consulta
                    }
                }

                MessageBox.Show("Alumno registrado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information); // Mostrar mensaje de éxito
                LimpiarValores(txtNombre, txtApellidos, txtNif, chkBaja); // Limpiar los TextBox y Checkbox

            }
            else
            {
                MessageBox.Show("Los campos están vacíos o no tienen el formato correcto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Mostrar mensaje de error si los campos no son validos
            }
            
        }

        // Metodo para limpiar valores de TextBox y CheckBox
        private void LimpiarValores(TextBox txtNombre, TextBox txtApellidos, TextBox txtNif, CheckBox chkBaja)
        {
            txtNombre.Clear();
            txtApellidos.Clear();
            txtNif.Clear();
            chkBaja.Checked = false;
        }

        // Metodo que comprueba si existe el registro y da de alta al alumno
        private bool AlumnoExiste(string nif)
        {
            using (OleDbConnection conn = ObtenerConexion()) // Establecer conexión con la base de datos
            {
                string query = "SELECT * FROM Alumnos WHERE Nif = @Nif";
                using (OleDbCommand cmd = new OleDbCommand(query, conn)) // Crear comando de inserción
                {
                    cmd.Parameters.AddWithValue("@Nif", nif); // Agregar el parámetro NIF

                    OleDbDataReader reader = cmd.ExecuteReader(); // Ejecutar la consulta y obtener los resultados

                    while (reader.Read())
                    {
                        query = "UPDATE Alumnos SET Baja = false WHERE Nif = @Nif";
                        using (OleDbCommand cmdUpdate = new OleDbCommand(query, conn)) // Crear comando de actualizacion
                        {
                            cmdUpdate.Parameters.AddWithValue("@Nif", nif); // Agregar el parámetro NIF
                            cmdUpdate.ExecuteNonQuery(); // Ejecutar actualizacion

                            return true;
                        }

                    }
                }
            }

            return false;
        }

        // Método para validar si los campos tienen el formato correcto
        private bool ValoresCorrectos(string nif, string nombre, string apellidos)
        {
            // Verifica si el NIF tiene 9 caracteres y si los campos no están vacíos
            if (!string.IsNullOrWhiteSpace(nif) &&
                nif.Length == 9 &&
                !string.IsNullOrWhiteSpace(nombre) &&
                !string.IsNullOrWhiteSpace(apellidos))
            {
                return true; // Los valores son correctos
            }

            return false; // Al menos uno de los valores es incorrecto
        }
        #endregion

        #region Modificar Alumno
        // Método para mostrar el formulario de modificación de alumnos
        private void MenuModificarAlumno_Click(object sender, EventArgs e)
        {
            MostrarFormularioAlumnosConGrid("Modificar Alumno", true, false, true); // Llamar a la función para mostrar el formulario con la opción de modificación
        }

        // Método para modificar los datos de los alumnos en el DataGridView
        private void ModificarAlumnos(DataGridView dgvAlumnos)
        {
            foreach (DataGridViewRow row in dgvAlumnos.SelectedRows) // Iterar sobre las filas seleccionadas
            {
                string nif = row.Cells["Nif"].Value.ToString(); // Obtener el NIF del alumno
                string nuevoNombre = row.Cells["Nombre"].Value.ToString(); // Obtener el nuevo nombre
                string nuevosApellidos = row.Cells["Apellidos"].Value.ToString(); // Obtener los nuevos apellidos
                bool nuevaBaja = Convert.ToBoolean(row.Cells["Baja"].Value); // Obtener el nuevo estado de baja

                using (OleDbConnection conn = ObtenerConexion()) // Establecer conexión con la base de datos
                {
                    string query = "UPDATE Alumnos SET Nombre = @Nombre, Apellidos = @Apellidos, Baja = @Baja WHERE Nif = @Nif"; // Consulta de actualización
                    using (OleDbCommand cmd = new OleDbCommand(query, conn)) // Crear comando de actualización
                    {
                        cmd.Parameters.AddWithValue("@Nif", nif); // Agregar el parámetro NIF
                        cmd.Parameters.AddWithValue("@Nombre", nuevoNombre); // Agregar el parámetro Nombre
                        cmd.Parameters.AddWithValue("@Apellidos", nuevosApellidos); // Agregar el parámetro Apellidos
                        cmd.Parameters.AddWithValue("@Baja", nuevaBaja); // Agregar el parámetro Baja
                        cmd.ExecuteNonQuery(); // Ejecutar la consulta
                    }
                }
            }

            MessageBox.Show("Alumnos modificados correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information); // Mostrar mensaje de éxito
        }
        #endregion

        #region Eliminar Alumno
        // Método para mostrar el formulario de eliminación de alumnos
        private void MenuEliminarAlumno_Click(object sender, EventArgs e)
        {
            MostrarFormularioAlumnosConGrid("Eliminar Alumno", false, true, false); // Llamar a la función para mostrar el formulario con la opción de eliminación
        }

        // Método para eliminar los alumnos seleccionados en el DataGridView
        private void EliminarAlumnos(DataGridView dgvAlumnos)
        {
            foreach (DataGridViewRow row in dgvAlumnos.SelectedRows) // Iterar sobre las filas seleccionadas
            {
                string nif = row.Cells["Nif"].Value.ToString(); // Obtener el NIF del alumno
                using (OleDbConnection conn = ObtenerConexion()) // Establecer conexión con la base de datos
                {
                    string query = "DELETE FROM Alumnos WHERE Nif = @Nif"; // Consulta de eliminación
                    using (OleDbCommand cmd = new OleDbCommand(query, conn)) // Crear comando de eliminación
                    {
                        cmd.Parameters.AddWithValue("@Nif", nif); // Agregar el parámetro NIF
                        cmd.ExecuteNonQuery(); // Ejecutar la consulta
                    }
                }

                dgvAlumnos.Rows.Remove(row); // Eliminar la fila del DataGridView
            }

            MessageBox.Show("Alumnos eliminados correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information); // Mostrar mensaje de éxito
        }
        #endregion

        #region Consultar Alumnos
        // Método para mostrar el formulario de consulta de alumnos
        private void MenuConsultarAlumnos_Click(object sender, EventArgs e)
        {
            MostrarFormularioAlumnosConGrid("Consultar Alumnos", false, false, false); // Llamar a la función para mostrar el formulario con la opción de consulta
        }
        #endregion

        #region Crear Evaluacion
        private void MenuCrearEvaluacion_Click(object sender, EventArgs e)
        {
            MostrarFormularioCrearEvaluacion();
        }

        // Metodo para mostrar el formulario de crear evaluacion
        private void MostrarFormularioCrearEvaluacion()
        {
            {
                // Limpiar el panel antes de agregar nuevos controles
                panel.Controls.Clear();
                panel.Padding = new Padding(20); // Margen interior del panel
                panel.BackColor = Color.White;  // Color de fondo opcional

                // Título centrado arriba
                Label lblTitulo = new Label()
                {
                    Text = "Crear Evaluación",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 18, FontStyle.Bold),
                    Dock = DockStyle.Top,
                };
                panel.Controls.Add(lblTitulo);

                // TableLayoutPanel para organizar los controles
                TableLayoutPanel tableLayout = new TableLayoutPanel()
                {
                    RowCount = 2,
                    ColumnCount = 2,
                    AutoSize = true,
                    Anchor = AnchorStyles.None,
                    Dock = DockStyle.None,
                    BackColor = Color.Transparent
                };

                // Columnas y filas
                tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F)); // Columna izquierda
                tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F)); // Columna derecha
                tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));     // Primera fila
                tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));     // Segunda fila (para TextBox grande)

                // Etiqueta Descripción
                Label lblDescripcion = new Label()
                {
                    Text = "Descripción:",
                    TextAlign = ContentAlignment.MiddleRight,
                    Anchor = AnchorStyles.Right | AnchorStyles.Top,
                    Padding = new Padding(0, 5, 0, 0)
                };

                // TextBox para la descripción
                TextBox txtDescripcion = new TextBox()
                {
                    Dock = DockStyle.Fill,
                    Multiline = true, // Permitir múltiples líneas
                    Height = 60,
                    Margin = new Padding(3, 5, 3, 3)
                };

                // Agregar controles a la tabla
                tableLayout.Controls.Add(lblDescripcion, 0, 0);
                tableLayout.Controls.Add(txtDescripcion, 1, 0);

                // Espacio en blanco en la última fila para mejor diseño
                tableLayout.Controls.Add(new Label() { Dock = DockStyle.Fill }, 0, 1);

                // Centrar la tabla en el panel
                tableLayout.Location = new Point(
                    (panel.Width - tableLayout.PreferredSize.Width) / 2 - 25,
                    lblTitulo.Bottom + 30
                );
                tableLayout.Anchor = AnchorStyles.None;

                panel.Controls.Add(tableLayout);

                // Botón Guardar centrado abajo
                Button btnGuardar = new Button()
                {
                    Text = "Guardar",
                    Width = 150,
                    Height = 40,
                    BackColor = Color.Black,
                    ForeColor = Color.White,
                    Dock = DockStyle.Bottom,
                    Margin = new Padding(10),
                    Anchor = AnchorStyles.None
                };

                // Evento Click del botón
                btnGuardar.Click += (s, e) => CrearEvaluacion(txtDescripcion);

                panel.Controls.Add(btnGuardar);
                btnGuardar.Left = (panel.Width - btnGuardar.Width) / 2; // Centramos el botón
                btnGuardar.Top = tableLayout.Bottom + 20; // Botón debajo de la tabla
            }
        }

        // Metodo para crear evaluacion
        private void CrearEvaluacion(TextBox txtDescripcion)
        {
            // Validación inicial: verificar si el campo está vacío
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("El campo descripción no puede estar vacío", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Comprobación si la evaluación ya existe
            if (EvaluacionExiste(txtDescripcion.Text.Trim()))
            {
                MessageBox.Show("Esta evaluación ya está dada de alta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Insertar la evaluación en la base de datos
            using (OleDbConnection conn = ObtenerConexion()) // Obtener la conexión con la base de datos
            {
                string query = "INSERT INTO Evaluaciones (Evaluacion) VALUES (@Evaluacion)";

                using (OleDbCommand cmd = new OleDbCommand(query, conn)) // Crear el comando SQL
                {
                    cmd.Parameters.AddWithValue("@Evaluacion", txtDescripcion.Text.Trim());

                        cmd.ExecuteNonQuery(); // Ejecutar la consulta de inserción
                        MessageBox.Show("Evaluación registrada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarValores(txtDescripcion, new TextBox(), new TextBox(), new CheckBox()); // Limpiar el campo descripción
                }
            }
        }

        private bool EvaluacionExiste(string descripcion)
        {
            using (OleDbConnection conn = ObtenerConexion()) // Obtener la conexión con la base de datos
            {
                string query = "SELECT COUNT(*) FROM Evaluaciones WHERE Evaluacion = @Evaluacion";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    // Asignar el parámetro para evitar inyecciones SQL
                    cmd.Parameters.AddWithValue("@Evaluacion", descripcion);

                    // Ejecutar la consulta y devolver si existe al menos un registro
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0; // Devuelve true si la descripción ya existe
                }
            }
        }
        #endregion

        #region Modificar Evaluacion
        private void MenuModificarEvaluacion_Click(object sender, EventArgs e)
        {
            MostrarFormularioModificarEvaluacion();
        }

        private void MostrarFormularioModificarEvaluacion()
        {
            // Limpiar el panel antes de agregar nuevos controles
            panel.Controls.Clear();
            panel.Padding = new Padding(20); // Margen interior del panel
            panel.BackColor = Color.White;  // Color de fondo opcional

            // Título centrado arriba
            Label lblTitulo = new Label()
            {
                Text = "Modificar Evaluación",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 18, FontStyle.Bold),
                Dock = DockStyle.Top,
            };
            panel.Controls.Add(lblTitulo);

            // TableLayoutPanel para organizar los controles
            TableLayoutPanel tableLayout = new TableLayoutPanel()
            {
                RowCount = 3,
                ColumnCount = 2,
                AutoSize = true,
                Anchor = AnchorStyles.None,
                Dock = DockStyle.None,
                BackColor = Color.Transparent
            };

            // Columnas y filas
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F)); // Columna izquierda
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F)); // Columna derecha
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));     // Primera fila
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));     // Segunda fila (para TextBox grande)
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));     // Tercera fila (espacio o botón)

            // Etiqueta Evaluación
            Label lblEvaluacion = new Label()
            {
                Text = "Evaluación:",
                TextAlign = ContentAlignment.MiddleRight,
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Padding = new Padding(0, 5, 0, 0)
            };

            // ComboBox para seleccionar evaluación
            ComboBox cmbEvaluaciones = new ComboBox()
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList, // Solo permite seleccionar valores
                Margin = new Padding(3, 5, 3, 3)
            };

            // Llenar el ComboBox con las descripciones existentes
            CargarEvaluaciones(cmbEvaluaciones);

            // Evento para cargar la descripción al seleccionar una evaluación
            TextBox txtDescripcion = new TextBox()
            {
                Dock = DockStyle.Fill,
                Multiline = true, // Permitir múltiples líneas
                Height = 60,
                Margin = new Padding(3, 5, 3, 3)
            };

            cmbEvaluaciones.SelectedIndexChanged += (s, e) =>
            {
                if (cmbEvaluaciones.SelectedItem != null)
                {
                    CargarDescripcionSeleccionada(cmbEvaluaciones.SelectedItem.ToString(), txtDescripcion);
                }
            };

            // Etiqueta Descripción
            Label lblDescripcion = new Label()
            {
                Text = "Descripción:",
                TextAlign = ContentAlignment.MiddleRight,
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Padding = new Padding(0, 5, 0, 0)
            };

            // Agregar controles a la tabla
            tableLayout.Controls.Add(lblEvaluacion, 0, 0);
            tableLayout.Controls.Add(cmbEvaluaciones, 1, 0);
            tableLayout.Controls.Add(lblDescripcion, 0, 1);
            tableLayout.Controls.Add(txtDescripcion, 1, 1);

            // Centrar la tabla en el panel
            tableLayout.Location = new Point(
                (panel.Width - tableLayout.PreferredSize.Width) / 2 - 25,
                lblTitulo.Bottom + 30
            );
            tableLayout.Anchor = AnchorStyles.None;

            panel.Controls.Add(tableLayout);

            // Botón Guardar centrado abajo
            Button btnGuardar = new Button()
            {
                Text = "Guardar",
                Width = 150,
                Height = 40,
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Bottom,
                Margin = new Padding(10),
                Anchor = AnchorStyles.None
            };

            // Evento Click del botón
            btnGuardar.Click += (s, e) => GuardarModificacion(cmbEvaluaciones, txtDescripcion);

            panel.Controls.Add(btnGuardar);
            btnGuardar.Left = (panel.Width - btnGuardar.Width) / 2; // Centramos el botón
            btnGuardar.Top = tableLayout.Bottom + 20; // Botón debajo de la tabla
        }

        // Metodo para actualizar el textBox con el valor elegido del comboBox
        private void CargarDescripcionSeleccionada(string evaluacion, TextBox txtDescripcion)
        {
            using (OleDbConnection conn = ObtenerConexion()) // Obtener la conexión
            {
                string query = "SELECT Evaluacion FROM Evaluaciones WHERE Evaluacion = @Evaluacion";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Evaluacion", evaluacion);

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtDescripcion.Text = reader["Evaluacion"].ToString(); // Cargar la descripción en el TextBox
                        }
                    }
                }
            }
        }


        // Metodo para actualizar la descripción
        private void GuardarModificacion(ComboBox cmbEvaluaciones, TextBox txtDescripcion)
        {
            if (cmbEvaluaciones.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione una evaluación para modificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string evaluacionOriginal = cmbEvaluaciones.SelectedItem.ToString();
            string nuevaDescripcion = txtDescripcion.Text.Trim();

            if (string.IsNullOrWhiteSpace(nuevaDescripcion))
            {
                MessageBox.Show("La descripción no puede estar vacía", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Comprobar si la nueva descripción ya existe
            if (EvaluacionExiste(nuevaDescripcion))
            {
                MessageBox.Show("Ya existe una evaluación con esta descripción", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (OleDbConnection conn = ObtenerConexion()) // Obtener la conexión
            {
                string query = "UPDATE Evaluaciones SET Evaluacion = @NuevaDescripcion WHERE Evaluacion = @EvaluacionOriginal";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NuevaDescripcion", nuevaDescripcion);
                    cmd.Parameters.AddWithValue("@EvaluacionOriginal", evaluacionOriginal);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Evaluación modificada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarEvaluaciones(cmbEvaluaciones); // Recargar ComboBox después de la modificación
                        txtDescripcion.Clear(); // Limpiar el TextBox
                    }
                    else
                    {
                        MessageBox.Show("No se pudo modificar la evaluación", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        #endregion

        #region Eliminar Evaluacion
        private void MenuEliminarEvaluacion_Click(object sender, EventArgs e)
        {
            MostrarFormularioEliminarEvaluacion();
        }

        // Metodo para mostrar el formulario de elminar evaluacion
        private void MostrarFormularioEliminarEvaluacion()
        {
            // Limpiar el panel antes de agregar nuevos controles
            panel.Controls.Clear();
            panel.Padding = new Padding(20); // Margen interior del panel
            panel.BackColor = Color.White;  // Color de fondo opcional

            // Título centrado arriba
            Label lblTitulo = new Label()
            {
                Text = "Eliminar Evaluación",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 18, FontStyle.Bold),
                Dock = DockStyle.Top,
            };
            panel.Controls.Add(lblTitulo);

            // TableLayoutPanel para organizar los controles
            TableLayoutPanel tableLayout = new TableLayoutPanel()
            {
                RowCount = 2,
                ColumnCount = 2,
                AutoSize = true,
                Anchor = AnchorStyles.None,
                Dock = DockStyle.None,
                BackColor = Color.Transparent
            };

            // Columnas y filas
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F)); // Columna izquierda
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F)); // Columna derecha
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));     // Primera fila
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));     // Segunda fila para botón

            // Etiqueta Evaluación
            Label lblEvaluacion = new Label()
            {
                Text = "Evaluación:",
                TextAlign = ContentAlignment.MiddleRight,
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Padding = new Padding(0, 5, 0, 0)
            };

            // ComboBox para seleccionar evaluación
            ComboBox cmbEvaluaciones = new ComboBox()
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList, // Solo permite seleccionar valores
                Margin = new Padding(3, 5, 3, 3)
            };

            // Llenar el ComboBox con las descripciones existentes
            CargarEvaluaciones(cmbEvaluaciones);

            // Agregar controles a la tabla
            tableLayout.Controls.Add(lblEvaluacion, 0, 0);
            tableLayout.Controls.Add(cmbEvaluaciones, 1, 0);

            // Centrar la tabla en el panel
            tableLayout.Location = new Point(
                (panel.Width - tableLayout.PreferredSize.Width) / 2 - 25,
                lblTitulo.Bottom + 30
            );
            tableLayout.Anchor = AnchorStyles.None;

            panel.Controls.Add(tableLayout);

            // Botón Eliminar centrado abajo
            Button btnEliminar = new Button()
            {
                Text = "Eliminar",
                Width = 150,
                Height = 40,
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Bottom,
                Margin = new Padding(10),
                Anchor = AnchorStyles.None
            };

            // Evento Click del botón
            btnEliminar.Click += (s, e) => EliminarEvaluacion(cmbEvaluaciones);

            panel.Controls.Add(btnEliminar);
            btnEliminar.Left = (panel.Width - btnEliminar.Width) / 2; // Centramos el botón
            btnEliminar.Top = tableLayout.Bottom + 20; // Botón debajo de la tabla
        }

        // Metodo para cargar evaluacion en el combobox
        private void CargarEvaluaciones(ComboBox cmbEvaluaciones)
        {
            cmbEvaluaciones.Items.Clear(); // Limpiar los valores previos

            using (OleDbConnection conn = ObtenerConexion()) // Obtener la conexión
            {
                string query = "SELECT Evaluacion FROM Evaluaciones"; // Consulta para obtener las descripciones
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbEvaluaciones.Items.Add(reader["Evaluacion"].ToString()); // Añadir cada descripción al ComboBox
                    }
                }
            }

            // Seleccionar el primer ítem por defecto si hay datos
            if (cmbEvaluaciones.Items.Count > 0)
                cmbEvaluaciones.SelectedIndex = 0;
        }

        // Metodo para eliminar la evaluacion seleccionada
        private void EliminarEvaluacion(ComboBox cmbEvaluaciones)
        {
            if (cmbEvaluaciones.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione una evaluación para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string evaluacion = cmbEvaluaciones.SelectedItem.ToString();

            using (OleDbConnection conn = ObtenerConexion()) // Obtener la conexión
            {
                string query = "DELETE FROM Evaluaciones WHERE Evaluacion = @Evaluacion";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Evaluacion", evaluacion);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Evaluación eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarEvaluaciones(cmbEvaluaciones); // Recargar ComboBox después de la eliminación
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la evaluación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        #region Consultar Evaluaciones
        // Método para mostrar el formulario de consulta de evaluaciones
        private void MenuConsultarEvaluaciones_Click(object sender, EventArgs e)
        {
            MostrarFormularioEvaluacionesConGrid("Consultar Evaluaciones", false); // Llamar a la función para mostrar el formulario con la opción de consulta
        }
        #endregion

        #region Consultar Notas
        private void MenuConsultarNotas_Click(object sender, EventArgs e)
        {
            MostrarFormularioConsultarNotas();
        }

        // Metodo para mostrar el formulario de consultar notas
        private void MostrarFormularioConsultarNotas()
        {
            // Limpiar el panel principal
            panel.Controls.Clear();
            panel.Padding = new Padding(20);
            panel.BackColor = Color.White;

            // Título principal centrado
            Label lblTitulo = new Label()
            {
                Text = "Consultar Notas",
                Font = new Font("Arial", 18, FontStyle.Bold),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 40
            };
            panel.Controls.Add(lblTitulo);

            // Panel contenedor principal con título "Selección de Alumnos"
            Panel contenedor = new Panel()
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(240, 240, 240),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Subtítulo arriba al centro
            Label lblSubtitulo = new Label()
            {
                Text = "Selección de Alumnos",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // TableLayoutPanel para filtros
            TableLayoutPanel tableLayout = new TableLayoutPanel()
            {
                RowCount = 4,
                ColumnCount = 2,
                AutoSize = true,
                Dock = DockStyle.Top,
                Padding = new Padding(5)
            };

            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));

            // ListBox Alumnos
            ListBox lstAlumnos = new ListBox() { Dock = DockStyle.Fill };
            CargarAlumnos(lstAlumnos);

            // CheckBox "Todos"
            CheckBox chkTodos = new CheckBox()
            {
                Text = "Todos",
                Dock = DockStyle.Left
            };
            chkTodos.CheckedChanged += (s, e) => lstAlumnos.Enabled = !chkTodos.Checked;

            // ComboBox Evaluaciones
            ComboBox cmbEvaluaciones = new ComboBox()
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            CargarEvaluaciones(cmbEvaluaciones);

            // Botón Consultar
            Button btnConsultar = new Button()
            {
                Text = "Consultar",
                BackColor = Color.Black,
                ForeColor = Color.White,
                Height = 30,
                Dock = DockStyle.Top
            };
            btnConsultar.Click += (s, e) =>
            {
                ConsultarNotas(lstAlumnos, chkTodos.Checked, cmbEvaluaciones.SelectedItem?.ToString());
            };

            // Agregar controles al TableLayoutPanel
            tableLayout.Controls.Add(new Label() { Text = "Alumnos:", TextAlign = ContentAlignment.MiddleRight }, 0, 0);
            tableLayout.Controls.Add(lstAlumnos, 1, 0);
            tableLayout.Controls.Add(new Label() { Dock = DockStyle.Fill }, 0, 1);
            tableLayout.Controls.Add(chkTodos, 1, 1);
            tableLayout.Controls.Add(new Label() { Text = "Evaluación:", TextAlign = ContentAlignment.MiddleRight }, 0, 2);
            tableLayout.Controls.Add(cmbEvaluaciones, 1, 2);
            tableLayout.Controls.Add(btnConsultar, 1, 3);

            // Agregar controles al contenedor principal
            contenedor.Controls.Add(tableLayout);
            contenedor.Controls.Add(lblSubtitulo);
            panel.Controls.Add(contenedor);

            // DataGridView con CRUD habilitado
            DataGridView dgvNotas = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = true,
                AllowUserToDeleteRows = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
                {
                    BackColor = Color.Black,
                    ForeColor = Color.White,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    Font = new Font("Arial", 10),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                EnableHeadersVisualStyles = false
            };
            LlenarDataGridViewNotas(dgvNotas);

            // Panel de botones CRUD
            Panel pnlBotones = new Panel()
            {
                Dock = DockStyle.Bottom,
                Height = 40
            };

            // Botón Guardar Cambios
            Button btnGuardar = new Button()
            {
                Text = "Guardar Cambios",
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Left,
                Width = 120
            };
            btnGuardar.Click += (s, e) => GuardarCambios(dgvNotas);

            // Botón Eliminar
            Button btnEliminar = new Button()
            {
                Text = "Eliminar",
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Right,
                Width = 120
            };
            btnEliminar.Click += (s, e) =>
            {
                if (dgvNotas.CurrentRow != null)
                {
                    dgvNotas.Rows.Remove(dgvNotas.CurrentRow);
                }
            };

            // Agregar botones al panel
            pnlBotones.Controls.Add(btnGuardar);
            pnlBotones.Controls.Add(btnEliminar);

            // Agregar controles al panel principal en orden correcto
            panel.Controls.Add(pnlBotones); // Botones en la parte inferior
            panel.Controls.Add(dgvNotas);   // DataGridView ocupa el espacio restante
        }



        // Método para llenar el DataGridView
        private void LlenarDataGridViewNotas(DataGridView dgvNotas)
        {
            using (OleDbConnection conn = ObtenerConexion())
            {
                string query = "SELECT * FROM Notas";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                DataTable dtNotas = new DataTable();
                adapter.Fill(dtNotas);

                dgvNotas.DataSource = dtNotas;
            }
        }

        // Método para guardar los cambios del DataGridView
        private void GuardarCambios(DataGridView dgvNotas)
        {
            using (OleDbConnection conn = ObtenerConexion())
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Notas", conn);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(adapter);
                DataTable dt = (DataTable)dgvNotas.DataSource;

                try
                {
                    adapter.Update(dt);
                    MessageBox.Show("Cambios guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar cambios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Metodo para cargar los alumnos en el ListBox
        private void CargarAlumnos(ListBox lstAlumnos)
        {
            using (OleDbConnection conn = ObtenerConexion())
            {
                string query = "SELECT Id, Apellidos, Nombre FROM Alumnos ORDER BY Apellidos, Nombre";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lstAlumnos.Items.Add($"{reader["Apellidos"]}, {reader["Nombre"]} ({reader["Id"]})");
                    }
                }
            }
        }

        // Metodo para consultar las notas
        private void ConsultarNotas(ListBox lstAlumnos, bool consultarTodos, string evaluacion)
        {
            // Validar si se seleccionó una evaluación
            if (string.IsNullOrWhiteSpace(evaluacion))
            {
                MessageBox.Show("Por favor, selecciona una evaluación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar si hay alumnos en el ListBox
            if (!consultarTodos && (lstAlumnos.Items.Count == 0 || lstAlumnos.SelectedItem == null))
            {
                MessageBox.Show("No hay alumnos disponibles o no se ha seleccionado ninguno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Obtener el Id de la evaluación a partir de su descripción
            int idEvaluacion;
            using (OleDbConnection conn = ObtenerConexion())
            {
                string evaluacionQuery = "SELECT Id FROM Evaluaciones WHERE Evaluacion = @Evaluacion";
                using (OleDbCommand cmd = new OleDbCommand(evaluacionQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Evaluacion", evaluacion);
                    object result = cmd.ExecuteScalar();

                    if (result == null)
                    {
                        MessageBox.Show("La evaluación seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    idEvaluacion = Convert.ToInt32(result);
                }
            }

            // Determinar si se consulta para un alumno específico o para todos
            string idAlumno = consultarTodos ? null : ObtenerIdAlumno(lstAlumnos.SelectedItem?.ToString());

            // Crear una conexión para consultar las notas
            using (OleDbConnection conn = ObtenerConexion())
            {
                string query = "SELECT Id_Alumno, Id_Evaluacion, DI, PMDM, AD FROM Notas WHERE Id_Evaluacion = @IdEvaluacion";
                if (!consultarTodos) query += " AND Id_Alumno = @IdAlumno";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdEvaluacion", idEvaluacion);
                    if (!consultarTodos) cmd.Parameters.AddWithValue("@IdAlumno", idAlumno);

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        // Verificar si hay resultados en la consulta
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No se encontraron notas para los criterios seleccionados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        // Verificar si `dgvNotas` está accesible y configurarlo
                        if (panel.Controls["dgvNotas"] is DataGridView dgvNotas)
                        {
                            dgvNotas.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("El DataGridView 'dgvNotas' no está disponible en el formulario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        // Metodo para obtener el id del alumno seleccionado
        private string ObtenerIdAlumno(string alumnoSeleccionado)
        {
            if (string.IsNullOrWhiteSpace(alumnoSeleccionado)) return null;
            int start = alumnoSeleccionado.LastIndexOf('(') + 1;
            int length = alumnoSeleccionado.LastIndexOf(')') - start;
            return alumnoSeleccionado.Substring(start, length);
        }

        #endregion

        #region Método común para los formularios con Grid
        // Método común para mostrar formularios con un DataGridView y botones
        private void MostrarFormularioAlumnosConGrid(string titulo, bool permitirEdicion, bool permitirEliminar, bool permitirModificar)
        {
            panel.Controls.Clear(); // Limpiar los controles existentes en el panel

            // Crear título para el formulario
            Label lblTitulo = new Label()
            {
                Text = titulo,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.Transparent,
                AutoSize = true
            };
            panel.Controls.Add(lblTitulo); // Agregar el título al panel
            AjustarCentrado(lblTitulo, 30, 10); // Ajustar el título en el centro

            // Mostrar lista de alumnos en un DataGridView
            string query = "SELECT * FROM Alumnos"; // Consulta para obtener los datos de los alumnos
            using (OleDbConnection conn = ObtenerConexion()) // Establecer conexión con la base de datos
            {
                OleDbCommand cmd = new OleDbCommand(query, conn); // Crear el comando de consulta
                OleDbDataReader reader = cmd.ExecuteReader(); // Ejecutar la consulta y obtener los resultados

                DataGridView dgvAlumnos = new DataGridView
                {
                    Left = 10, // Establecer un margen izquierdo
                    Top = lblTitulo.Bottom + 10, // Margen superior debajo del título
                    Width = panel.Width - 20, // Establecer el ancho al máximo del panel menos el margen
                    Height = panel.Height - 120, // Establecer la altura para que ocupe casi toda la altura de la ventana, dejando espacio para los controles
                    AllowUserToAddRows = true, // Permitir fila vacía al final
                    ReadOnly = !permitirEdicion, // Deshabilitar la edición si es solo consulta
                };
                panel.Controls.Add(dgvAlumnos); // Agregar el DataGridView al panel

                // Configurar las columnas del DataGridView
                dgvAlumnos.Columns.Add("Id", "ID");
                dgvAlumnos.Columns.Add("Nombre", "Nombre");
                dgvAlumnos.Columns.Add("Apellidos", "Apellidos");
                dgvAlumnos.Columns.Add("Nif", "NIF");
                dgvAlumnos.Columns.Add("Baja", "Baja");

                // Agregar los datos de los alumnos al DataGridView
                while (reader.Read())
                {
                    dgvAlumnos.Rows.Add(reader["Id"], reader["Nombre"], reader["Apellidos"], reader["Nif"], reader["Baja"]);
                }

                // Ajustar el tamaño de las columnas automáticamente
                dgvAlumnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Si se permite eliminar, agregar un botón de eliminar
                if (permitirEliminar)
                {
                    Button btnEliminar = new Button
                    {
                        Text = "Eliminar",
                        Width = 150,
                        Height = 40,
                        BackColor = System.Drawing.Color.Black,
                        ForeColor = System.Drawing.Color.White
                    };
                    btnEliminar.Click += (s, e) => EliminarAlumnos(dgvAlumnos); // Al hacer clic, eliminar los alumnos seleccionados
                    panel.Controls.Add(btnEliminar); // Agregar el botón al panel
                    btnEliminar.Left = (panel.Width - btnEliminar.Width) / 2; // Centramos el botón
                    btnEliminar.Top = dgvAlumnos.Bottom + 10; // Botón debajo del grid
                }

                // Si se permite modificar, agregar un botón de guardar cambios
                if (permitirModificar)
                {
                    Button btnGuardar = new Button
                    {
                        Text = "Guardar cambios",
                        Width = 150,
                        Height = 40,
                        BackColor = System.Drawing.Color.Black,
                        ForeColor = System.Drawing.Color.White
                    };
                    btnGuardar.Click += (s, e) => ModificarAlumnos(dgvAlumnos); // Al hacer clic, guardar los cambios
                    panel.Controls.Add(btnGuardar); // Agregar el botón al panel
                    btnGuardar.Left = (panel.Width - btnGuardar.Width) / 2; // Centramos el botón
                    btnGuardar.Top = dgvAlumnos.Bottom + 10; // Botón debajo del grid
                }
            }
        }

        private void MostrarFormularioEvaluacionesConGrid(string titulo, bool permitirEdicion)
        {
            panel.Controls.Clear(); // Limpiar los controles existentes en el panel

            // Crear título para el formulario
            Label lblTitulo = new Label()
            {
                Text = titulo,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.Transparent,
                AutoSize = true
            };
            panel.Controls.Add(lblTitulo); // Agregar el título al panel
            AjustarCentrado(lblTitulo, 30, 10); // Ajustar el título en el centro

            // Mostrar lista de alumnos en un DataGridView
            string query = "SELECT * FROM Evaluaciones"; // Consulta para obtener los datos de los alumnos
            using (OleDbConnection conn = ObtenerConexion()) // Establecer conexión con la base de datos
            {
                OleDbCommand cmd = new OleDbCommand(query, conn); // Crear el comando de consulta
                OleDbDataReader reader = cmd.ExecuteReader(); // Ejecutar la consulta y obtener los resultados

                DataGridView dgvEvaluaciones = new DataGridView
                {
                    Left = 10, // Establecer un margen izquierdo
                    Top = lblTitulo.Bottom + 10, // Margen superior debajo del título
                    Width = panel.Width - 20, // Establecer el ancho al máximo del panel menos el margen
                    Height = panel.Height - 120, // Establecer la altura para que ocupe casi toda la altura de la ventana, dejando espacio para los controles
                    AllowUserToAddRows = true, // Permitir fila vacía al final
                    ReadOnly = !permitirEdicion, // Deshabilitar la edición si es solo consulta
                };
                panel.Controls.Add(dgvEvaluaciones); // Agregar el DataGridView al panel

                // Configurar las columnas del DataGridView
                dgvEvaluaciones.Columns.Add("Id", "ID");
                dgvEvaluaciones.Columns.Add("Evaluacion", "Evaluacion");

                // Agregar los datos de los alumnos al DataGridView
                while (reader.Read())
                {
                    dgvEvaluaciones.Rows.Add(reader["Id"], reader["Evaluacion"]);
                }

                // Ajustar el tamaño de las columnas automáticamente
                dgvEvaluaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        #endregion

        #region Funciones de Centrado y Resize
        // Método para centrar los controles
        private void AjustarCentrado(Control ctrl, int left, int top)
        {
            ctrl.Left = (panel.Width - ctrl.Width) / 2; // Centrar horizontalmente
            ctrl.Top = top; // Establecer el margen superior
        }

        // Método para redimensionar los controles cuando la ventana cambie de tamaño
        private void Formulario_Resize(object sender, EventArgs e)
        {
            // Ajustar el tamaño y la posición del panel cuando el formulario cambie de tamaño
            panel.Width = this.ClientSize.Width - 30;
            panel.Height = this.ClientSize.Height - 50;

            // Redimensionar y reposicionar los controles dentro del panel de manera responsiva
            foreach (Control ctrl in panel.Controls)
            {
                // Si el control es un DataGridView, se ajusta su tamaño para que ocupe el espacio disponible
                if (ctrl is DataGridView dgvAlumnos)
                {
                    dgvAlumnos.Width = panel.Width - 20;
                    dgvAlumnos.Height = panel.Height - 120;
                }

                // Si el control es un botón, se ajusta su posición y tamaño
                if (ctrl is Button btn)
                {
                    btn.Left = (panel.Width - btn.Width) / 2;
                    btn.Top = panel.Height - btn.Height - 10;
                }

                // Si el control es un Label o TextBox, se ajusta su posición de manera responsiva
                if (ctrl is Label lbl)
                {
                    // Si el texto del Label es uno de los títulos, centrarlo
                    if (lbl.Text == "Crear Alumno" || lbl.Text == "Modificar Alumno" || lbl.Text == "Eliminar Alumno" || lbl.Text == "Consultar Alumnos")
                    {
                        AjustarCentrado(lbl, 30, 10); // Centrar el título
                    }

                    // Si es un Label genérico, ajustar el tamaño
                    else
                    {
                        lbl.Width = panel.Width - 40; // Ajustar el ancho para que ocupe casi todo el panel, dejando un margen
                    }
                }
            }
        }

        #endregion


    }
}