namespace ANN_GUI_SEM5_BINUS
{
    partial class Form1
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
            this.lbl_uploaded = new System.Windows.Forms.Label();
            this.grpbox_action = new System.Windows.Forms.GroupBox();
            this.btn_upload = new System.Windows.Forms.Button();
            this.btn_removeimages = new System.Windows.Forms.Button();
            this.btn_browseimages = new System.Windows.Forms.Button();
            this.open = new System.Windows.Forms.GroupBox();
            this.btn_searchsimilar = new System.Windows.Forms.Button();
            this.btn_classifyitems = new System.Windows.Forms.Button();
            this.listView_uploaded = new System.Windows.Forms.ListView();
            this.listView_selectedimage = new System.Windows.Forms.ListView();
            this.lbl_itemtype = new System.Windows.Forms.Label();
            this.lbl_selectedimage = new System.Windows.Forms.Label();
            this.cb_type = new System.Windows.Forms.ComboBox();
            this.imagelist_selected = new System.Windows.Forms.ImageList(this.components);
            this.imageList_uploaded = new System.Windows.Forms.ImageList(this.components);
            this.grpbox_action.SuspendLayout();
            this.open.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_uploaded
            // 
            this.lbl_uploaded.AutoSize = true;
            this.lbl_uploaded.Location = new System.Drawing.Point(12, 9);
            this.lbl_uploaded.Name = "lbl_uploaded";
            this.lbl_uploaded.Size = new System.Drawing.Size(73, 17);
            this.lbl_uploaded.TabIndex = 0;
            this.lbl_uploaded.Text = "Uploaded:";
            // 
            // grpbox_action
            // 
            this.grpbox_action.Controls.Add(this.btn_upload);
            this.grpbox_action.Controls.Add(this.btn_removeimages);
            this.grpbox_action.Controls.Add(this.btn_browseimages);
            this.grpbox_action.Location = new System.Drawing.Point(366, 311);
            this.grpbox_action.Name = "grpbox_action";
            this.grpbox_action.Size = new System.Drawing.Size(181, 162);
            this.grpbox_action.TabIndex = 1;
            this.grpbox_action.TabStop = false;
            this.grpbox_action.Text = "Action";
            // 
            // btn_upload
            // 
            this.btn_upload.Location = new System.Drawing.Point(20, 108);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(135, 23);
            this.btn_upload.TabIndex = 2;
            this.btn_upload.Text = "Upload";
            this.btn_upload.UseVisualStyleBackColor = true;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // btn_removeimages
            // 
            this.btn_removeimages.Location = new System.Drawing.Point(20, 73);
            this.btn_removeimages.Name = "btn_removeimages";
            this.btn_removeimages.Size = new System.Drawing.Size(135, 23);
            this.btn_removeimages.TabIndex = 1;
            this.btn_removeimages.Text = "Remove Images";
            this.btn_removeimages.UseVisualStyleBackColor = true;
            this.btn_removeimages.Click += new System.EventHandler(this.btn_removeimages_Click);
            // 
            // btn_browseimages
            // 
            this.btn_browseimages.Location = new System.Drawing.Point(20, 38);
            this.btn_browseimages.Name = "btn_browseimages";
            this.btn_browseimages.Size = new System.Drawing.Size(135, 23);
            this.btn_browseimages.TabIndex = 0;
            this.btn_browseimages.Text = "Browse Images";
            this.btn_browseimages.UseVisualStyleBackColor = true;
            this.btn_browseimages.Click += new System.EventHandler(this.btn_browseimages_Click);
            // 
            // open
            // 
            this.open.Controls.Add(this.btn_searchsimilar);
            this.open.Controls.Add(this.btn_classifyitems);
            this.open.Location = new System.Drawing.Point(563, 311);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(225, 162);
            this.open.TabIndex = 2;
            this.open.TabStop = false;
            this.open.Text = "Open";
            // 
            // btn_searchsimilar
            // 
            this.btn_searchsimilar.Location = new System.Drawing.Point(22, 88);
            this.btn_searchsimilar.Name = "btn_searchsimilar";
            this.btn_searchsimilar.Size = new System.Drawing.Size(177, 23);
            this.btn_searchsimilar.TabIndex = 1;
            this.btn_searchsimilar.Text = "Search Similar Images";
            this.btn_searchsimilar.UseVisualStyleBackColor = true;
            // 
            // btn_classifyitems
            // 
            this.btn_classifyitems.Location = new System.Drawing.Point(22, 58);
            this.btn_classifyitems.Name = "btn_classifyitems";
            this.btn_classifyitems.Size = new System.Drawing.Size(177, 23);
            this.btn_classifyitems.TabIndex = 0;
            this.btn_classifyitems.Text = "Classify Items";
            this.btn_classifyitems.UseVisualStyleBackColor = true;
            // 
            // listView_uploaded
            // 
            this.listView_uploaded.Location = new System.Drawing.Point(15, 29);
            this.listView_uploaded.Name = "listView_uploaded";
            this.listView_uploaded.Size = new System.Drawing.Size(773, 216);
            this.listView_uploaded.TabIndex = 3;
            this.listView_uploaded.UseCompatibleStateImageBehavior = false;
            this.listView_uploaded.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // listView_selectedimage
            // 
            this.listView_selectedimage.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listView_selectedimage.Location = new System.Drawing.Point(15, 311);
            this.listView_selectedimage.Name = "listView_selectedimage";
            this.listView_selectedimage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listView_selectedimage.Size = new System.Drawing.Size(326, 162);
            this.listView_selectedimage.TabIndex = 4;
            this.listView_selectedimage.UseCompatibleStateImageBehavior = false;
            // 
            // lbl_itemtype
            // 
            this.lbl_itemtype.AutoSize = true;
            this.lbl_itemtype.Location = new System.Drawing.Point(366, 252);
            this.lbl_itemtype.Name = "lbl_itemtype";
            this.lbl_itemtype.Size = new System.Drawing.Size(74, 17);
            this.lbl_itemtype.TabIndex = 5;
            this.lbl_itemtype.Text = "Item Type:";
            // 
            // lbl_selectedimage
            // 
            this.lbl_selectedimage.AutoSize = true;
            this.lbl_selectedimage.Location = new System.Drawing.Point(12, 283);
            this.lbl_selectedimage.Name = "lbl_selectedimage";
            this.lbl_selectedimage.Size = new System.Drawing.Size(109, 17);
            this.lbl_selectedimage.TabIndex = 6;
            this.lbl_selectedimage.Text = "Selected Image:";
            // 
            // cb_type
            // 
            this.cb_type.FormattingEnabled = true;
            this.cb_type.Items.AddRange(new object[] {
            "Stationary",
            "Artisan Tool",
            "Cutlery",
            "Cooking Ware"});
            this.cb_type.Location = new System.Drawing.Point(366, 280);
            this.cb_type.Name = "cb_type";
            this.cb_type.Size = new System.Drawing.Size(422, 24);
            this.cb_type.TabIndex = 7;
            this.cb_type.Text = "--choose--";
            this.cb_type.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // imagelist_selected
            // 
            this.imagelist_selected.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imagelist_selected.ImageSize = new System.Drawing.Size(16, 16);
            this.imagelist_selected.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList_uploaded
            // 
            this.imageList_uploaded.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList_uploaded.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList_uploaded.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 485);
            this.Controls.Add(this.cb_type);
            this.Controls.Add(this.lbl_selectedimage);
            this.Controls.Add(this.lbl_itemtype);
            this.Controls.Add(this.listView_selectedimage);
            this.Controls.Add(this.listView_uploaded);
            this.Controls.Add(this.open);
            this.Controls.Add(this.grpbox_action);
            this.Controls.Add(this.lbl_uploaded);
            this.Name = "Form1";
            this.Text = "BlueStore";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpbox_action.ResumeLayout(false);
            this.open.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_uploaded;
        private System.Windows.Forms.GroupBox grpbox_action;
        private System.Windows.Forms.GroupBox open;
        private System.Windows.Forms.ListView listView_uploaded;
        private System.Windows.Forms.ListView listView_selectedimage;
        private System.Windows.Forms.Label lbl_itemtype;
        private System.Windows.Forms.Label lbl_selectedimage;
        private System.Windows.Forms.ComboBox cb_type;
        private System.Windows.Forms.Button btn_upload;
        private System.Windows.Forms.Button btn_removeimages;
        private System.Windows.Forms.Button btn_browseimages;
        private System.Windows.Forms.Button btn_searchsimilar;
        private System.Windows.Forms.Button btn_classifyitems;
        private System.Windows.Forms.ImageList imagelist_selected;
        private System.Windows.Forms.ImageList imageList_uploaded;
    }
}

