namespace practiquesIEI
{
    partial class Búsqueda
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tbLocalidad = new System.Windows.Forms.TextBox();
            this.tbCP = new System.Windows.Forms.TextBox();
            this.tbProv = new System.Windows.Forms.TextBox();
            this.cbTipo = new System.Windows.Forms.ComboBox();
            this.tbLogs = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btAceptar = new System.Windows.Forms.Button();
            this.btCancelar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btCarga = new System.Windows.Forms.Button();
            this.btCerrar = new System.Windows.Forms.Button();
            this.wbMapa = new System.Windows.Forms.WebBrowser();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbLocalidad
            // 
            this.tbLocalidad.Location = new System.Drawing.Point(129, 25);
            this.tbLocalidad.Name = "tbLocalidad";
            this.tbLocalidad.Size = new System.Drawing.Size(259, 22);
            this.tbLocalidad.TabIndex = 0;
            // 
            // tbCP
            // 
            this.tbCP.Location = new System.Drawing.Point(129, 62);
            this.tbCP.Name = "tbCP";
            this.tbCP.Size = new System.Drawing.Size(259, 22);
            this.tbCP.TabIndex = 1;
            // 
            // tbProv
            // 
            this.tbProv.Location = new System.Drawing.Point(129, 100);
            this.tbProv.Name = "tbProv";
            this.tbProv.Size = new System.Drawing.Size(259, 22);
            this.tbProv.TabIndex = 2;
            // 
            // cbTipo
            // 
            this.cbTipo.FormattingEnabled = true;
            this.cbTipo.Location = new System.Drawing.Point(129, 137);
            this.cbTipo.Name = "cbTipo";
            this.cbTipo.Size = new System.Drawing.Size(259, 24);
            this.cbTipo.TabIndex = 3;
            this.cbTipo.SelectedIndexChanged += new System.EventHandler(this.cbTipo_SelectedIndexChanged);
            // 
            // tbLogs
            // 
            this.tbLogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLogs.Location = new System.Drawing.Point(46, 361);
            this.tbLogs.Multiline = true;
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.ReadOnly = true;
            this.tbLogs.Size = new System.Drawing.Size(447, 304);
            this.tbLogs.TabIndex = 4;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btAceptar);
            this.groupBox1.Controls.Add(this.btCancelar);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbProv);
            this.groupBox1.Controls.Add(this.tbLocalidad);
            this.groupBox1.Controls.Add(this.tbCP);
            this.groupBox1.Controls.Add(this.cbTipo);
            this.groupBox1.Location = new System.Drawing.Point(47, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(447, 234);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btAceptar
            // 
            this.btAceptar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btAceptar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btAceptar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btAceptar.Location = new System.Drawing.Point(228, 182);
            this.btAceptar.Name = "btAceptar";
            this.btAceptar.Size = new System.Drawing.Size(100, 33);
            this.btAceptar.TabIndex = 9;
            this.btAceptar.Text = "Aceptar";
            this.btAceptar.UseVisualStyleBackColor = false;
            this.btAceptar.Click += new System.EventHandler(this.btAceptar_Click);
            // 
            // btCancelar
            // 
            this.btCancelar.Location = new System.Drawing.Point(122, 182);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(100, 33);
            this.btCancelar.TabIndex = 8;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.UseVisualStyleBackColor = true;
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "Cos. Postal";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Provincia";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tipo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Localidad";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(352, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(439, 38);
            this.label1.TabIndex = 8;
            this.label1.Text = "Buscador centros educativos";
            // 
            // btCarga
            // 
            this.btCarga.Location = new System.Drawing.Point(47, 689);
            this.btCarga.Name = "btCarga";
            this.btCarga.Size = new System.Drawing.Size(189, 36);
            this.btCarga.TabIndex = 9;
            this.btCarga.Text = "Carga de datos";
            this.btCarga.UseVisualStyleBackColor = true;
            this.btCarga.Click += new System.EventHandler(this.btCarga_Click);
            // 
            // btCerrar
            // 
            this.btCerrar.Location = new System.Drawing.Point(255, 689);
            this.btCerrar.Name = "btCerrar";
            this.btCerrar.Size = new System.Drawing.Size(189, 36);
            this.btCerrar.TabIndex = 10;
            this.btCerrar.Text = "Cerrar";
            this.btCerrar.UseVisualStyleBackColor = true;
            this.btCerrar.Click += new System.EventHandler(this.btCerrar_Click);
            // 
            // wbMapa
            // 
            this.wbMapa.Location = new System.Drawing.Point(525, 105);
            this.wbMapa.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbMapa.Name = "wbMapa";
            this.wbMapa.Size = new System.Drawing.Size(615, 560);
            this.wbMapa.TabIndex = 11;
            // 
            // Búsqueda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 743);
            this.Controls.Add(this.wbMapa);
            this.Controls.Add(this.btCerrar);
            this.Controls.Add(this.btCarga);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbLogs);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Búsqueda";
            this.Text = "Búsqueda";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbLocalidad;
        private System.Windows.Forms.TextBox tbCP;
        private System.Windows.Forms.TextBox tbProv;
        private System.Windows.Forms.ComboBox cbTipo;
        private System.Windows.Forms.TextBox tbLogs;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btAceptar;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.Button btCarga;
        private System.Windows.Forms.Button btCerrar;
        private System.Windows.Forms.WebBrowser wbMapa;
    }
}