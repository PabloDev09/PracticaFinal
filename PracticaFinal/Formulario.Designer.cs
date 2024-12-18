using System;
using System.Drawing;
using System.Windows.Forms;

namespace PracticaFinal
{
    partial class Formulario
    {
        private System.ComponentModel.IContainer components = null;

        // Menú
        private MenuStrip menu;
        private ToolStripMenuItem menuAlumnos;
        private ToolStripMenuItem menuCrearAlumno;
        private ToolStripMenuItem menuModificarAlumno;
        private ToolStripMenuItem menuEliminarAlumno;
        private ToolStripMenuItem menuConsultarAlumnos;

        // Menú Evaluaciones
        private ToolStripMenuItem menuEvaluaciones;
        private ToolStripMenuItem menuCrearEvaluacion;
        private ToolStripMenuItem menuModificarEvaluacion;
        private ToolStripMenuItem menuEliminarEvaluacion;
        private ToolStripMenuItem menuConsultarEvaluaciones;

        // Menú Notas
        private ToolStripMenuItem menuNotas;
        private ToolStripMenuItem menuConsultarNotas;

        // Panel de contenido
        private Panel panel;

        // Constructor de Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Método de inicialización del formulario
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Formulario));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuAlumnos = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCrearAlumno = new System.Windows.Forms.ToolStripMenuItem();
            this.menuModificarAlumno = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEliminarAlumno = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConsultarAlumnos = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEvaluaciones = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCrearEvaluacion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuModificarEvaluacion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEliminarEvaluacion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConsultarEvaluaciones = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNotas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConsultarNotas = new System.Windows.Forms.ToolStripMenuItem();
            this.panel = new System.Windows.Forms.Panel();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.Black;
            this.menu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.menu.ForeColor = System.Drawing.Color.White;
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAlumnos,
            this.menuEvaluaciones,
            this.menuNotas});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(782, 31);
            this.menu.TabIndex = 0;
            this.menu.Text = "menu";
            // 
            // menuAlumnos
            // 
            this.menuAlumnos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCrearAlumno,
            this.menuModificarAlumno,
            this.menuEliminarAlumno,
            this.menuConsultarAlumnos});
            this.menuAlumnos.Name = "menuAlumnos";
            this.menuAlumnos.Size = new System.Drawing.Size(91, 27);
            this.menuAlumnos.Text = "Alumnos";
            // 
            // menuCrearAlumno
            // 
            this.menuCrearAlumno.Name = "menuCrearAlumno";
            this.menuCrearAlumno.Size = new System.Drawing.Size(239, 28);
            this.menuCrearAlumno.Text = "Crear Alumno";
            this.menuCrearAlumno.Click += new System.EventHandler(this.MenuCrearAlumno_Click);
            // 
            // menuModificarAlumno
            // 
            this.menuModificarAlumno.Name = "menuModificarAlumno";
            this.menuModificarAlumno.Size = new System.Drawing.Size(239, 28);
            this.menuModificarAlumno.Text = "Modificar Alumno";
            this.menuModificarAlumno.Click += new System.EventHandler(this.MenuModificarAlumno_Click);
            // 
            // menuEliminarAlumno
            // 
            this.menuEliminarAlumno.Name = "menuEliminarAlumno";
            this.menuEliminarAlumno.Size = new System.Drawing.Size(239, 28);
            this.menuEliminarAlumno.Text = "Eliminar Alumno";
            this.menuEliminarAlumno.Click += new System.EventHandler(this.MenuEliminarAlumno_Click);
            // 
            // menuConsultarAlumnos
            // 
            this.menuConsultarAlumnos.Name = "menuConsultarAlumnos";
            this.menuConsultarAlumnos.Size = new System.Drawing.Size(239, 28);
            this.menuConsultarAlumnos.Text = "Consultar Alumnos";
            this.menuConsultarAlumnos.Click += new System.EventHandler(this.MenuConsultarAlumnos_Click);
            // 
            // menuEvaluaciones
            // 
            this.menuEvaluaciones.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCrearEvaluacion,
            this.menuModificarEvaluacion,
            this.menuEliminarEvaluacion,
            this.menuConsultarEvaluaciones});
            this.menuEvaluaciones.Name = "menuEvaluaciones";
            this.menuEvaluaciones.Size = new System.Drawing.Size(121, 27);
            this.menuEvaluaciones.Text = "Evaluaciones";
            // 
            // menuCrearEvaluacion
            // 
            this.menuCrearEvaluacion.Name = "menuCrearEvaluacion";
            this.menuCrearEvaluacion.Size = new System.Drawing.Size(269, 28);
            this.menuCrearEvaluacion.Text = "Crear Evaluación";
            this.menuCrearEvaluacion.Click += new System.EventHandler(this.MenuCrearEvaluacion_Click);
            // 
            // menuModificarEvaluacion
            // 
            this.menuModificarEvaluacion.Name = "menuModificarEvaluacion";
            this.menuModificarEvaluacion.Size = new System.Drawing.Size(269, 28);
            this.menuModificarEvaluacion.Text = "Modificar Evaluación";
            this.menuModificarEvaluacion.Click += new System.EventHandler(this.MenuModificarEvaluacion_Click);
            // 
            // menuEliminarEvaluacion
            // 
            this.menuEliminarEvaluacion.Name = "menuEliminarEvaluacion";
            this.menuEliminarEvaluacion.Size = new System.Drawing.Size(269, 28);
            this.menuEliminarEvaluacion.Text = "Eliminar Evaluación";
            this.menuEliminarEvaluacion.Click += new System.EventHandler(this.MenuEliminarEvaluacion_Click);
            // 
            // menuConsultarEvaluaciones
            // 
            this.menuConsultarEvaluaciones.Name = "menuConsultarEvaluaciones";
            this.menuConsultarEvaluaciones.Size = new System.Drawing.Size(269, 28);
            this.menuConsultarEvaluaciones.Text = "Consultar Evaluaciones";
            this.menuConsultarEvaluaciones.Click += new System.EventHandler(this.MenuConsultarEvaluaciones_Click);
            // 
            // menuNotas
            // 
            this.menuNotas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuConsultarNotas});
            this.menuNotas.Name = "menuNotas";
            this.menuNotas.Size = new System.Drawing.Size(69, 27);
            this.menuNotas.Text = "Notas";
            // 
            // menuConsultarNotas
            // 
            this.menuConsultarNotas.Name = "menuConsultarNotas";
            this.menuConsultarNotas.Size = new System.Drawing.Size(224, 28);
            this.menuConsultarNotas.Text = "Consultar Notas";
            this.menuConsultarNotas.Click += new System.EventHandler(this.MenuConsultarNotas_Click);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.White;
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 31);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(782, 372);
            this.panel.TabIndex = 1;
            // 
            // Formulario
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(782, 403);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "Formulario";
            this.Text = "Gestión Alumnos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}
