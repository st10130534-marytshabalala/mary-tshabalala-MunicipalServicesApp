using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace mary_tshabalala_MunicipalServicesApp
{
    public partial class ServiceRequestStatusForm : Form
    {
        private BindingList<ServiceRequest> serviceRequests;

        public ServiceRequestStatusForm()
        {
            InitializeComponent();
            serviceRequests = new BindingList<ServiceRequest>();
            serviceRequestsDataGridView.DataSource = serviceRequests;
        }

        private void InitializeComponent()
        {
            this.serviceRequestsDataGridView = new System.Windows.Forms.DataGridView();
            this.filterTextBox = new System.Windows.Forms.TextBox();
            this.filterButton = new System.Windows.Forms.Button();
            this.updateStatusButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.serviceRequestsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // serviceRequestsDataGridView
            // 
            this.serviceRequestsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.serviceRequestsDataGridView.Location = new System.Drawing.Point(12, 39);
            this.serviceRequestsDataGridView.Name = "serviceRequestsDataGridView";
            this.serviceRequestsDataGridView.Size = new System.Drawing.Size(776, 350);
            this.serviceRequestsDataGridView.TabIndex = 0;
            this.serviceRequestsDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.serviceRequestsDataGridView_CellDoubleClick);
            // 
            // filterTextBox
            // 
            this.filterTextBox.Location = new System.Drawing.Point(12, 12);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(200, 20);
            this.filterTextBox.TabIndex = 1;
            this.filterTextBox.TextChanged += new System.EventHandler(this.filterTextBox_TextChanged);
            // 
            // filterButton
            // 
            this.filterButton.Location = new System.Drawing.Point(218, 10);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(75, 23);
            this.filterButton.TabIndex = 2;
            this.filterButton.Text = "Filter";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.filterButton_Click);
            // 
            // updateStatusButton
            // 
            this.updateStatusButton.Location = new System.Drawing.Point(713, 10);
            this.updateStatusButton.Name = "updateStatusButton";
            this.updateStatusButton.Size = new System.Drawing.Size(75, 23);
            this.updateStatusButton.TabIndex = 3;
            this.updateStatusButton.Text = "Update Status";
            this.updateStatusButton.UseVisualStyleBackColor = true;
            this.updateStatusButton.Click += new System.EventHandler(this.updateStatusButton_Click);
            // 
            // ServiceRequestStatusForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 401);
            this.Controls.Add(this.updateStatusButton);
            this.Controls.Add(this.filterButton);
            this.Controls.Add(this.filterTextBox);
            this.Controls.Add(this.serviceRequestsDataGridView);
            this.Name = "ServiceRequestStatusForm";
            this.Text = "Service Request Status";
            this.Load += new System.EventHandler(this.ServiceRequestStatusForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.serviceRequestsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ServiceRequestStatusForm_Load(object sender, EventArgs e)
        {
            LoadServiceRequests();
        }

        private void LoadServiceRequests()
        {
            // Retrieve service requests from the data source and populate the BindingList
            serviceRequests.Clear();
            serviceRequests.Add(new ServiceRequest { Id = 1, Description = "Pothole repair", Status = "Pending" });
            serviceRequests.Add(new ServiceRequest { Id = 2, Description = "Streetlight outage", Status = "In Progress" });
            serviceRequests.Add(new ServiceRequest { Id = 3, Description = "Trash collection issue", Status = "Completed" });
        }

        private void filterTextBox_TextChanged(object sender, EventArgs e)
        {
            // Filter the service requests based on the text in the filterTextBox
            UpdateFilteredServiceRequests();
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            // Refresh the filtered service requests
            UpdateFilteredServiceRequests();
        }

        private void UpdateFilteredServiceRequests()
        {
            // Apply the filter to the BindingList and update the DataGridView
            string filterText = filterTextBox.Text.ToLower();
            serviceRequestsDataGridView.DataSource = new BindingList<ServiceRequest>(serviceRequests.FindAll(sr => sr.Description.ToLower().Contains(filterText)));
        }

        private void serviceRequestsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle the double-click event to view the details of the selected service request
            ServiceRequest selectedRequest = (ServiceRequest)serviceRequestsDataGridView.CurrentRow.DataBoundItem;
            ShowServiceRequestDetails(selectedRequest);
        }

        private void ShowServiceRequestDetails(ServiceRequest serviceRequest)
        {
            // Create a new form or dialog to display the details of the selected service request
            ServiceRequestDetailsForm detailsForm = new ServiceRequestDetailsForm(serviceRequest);
            detailsForm.ShowDialog();
        }

        private void updateStatusButton_Click(object sender, EventArgs e)
        {
            // Handle the update status button click event
            ServiceRequest selectedRequest = (ServiceRequest)serviceRequestsDataGridView.CurrentRow.DataBoundItem;
            UpdateServiceRequestStatus(selectedRequest);
        }

        private void UpdateServiceRequestStatus(ServiceRequest serviceRequest)
        {
            // Display a form or dialog to allow the user to update the status of the selected service request
            // Update the status of the serviceRequest object and the BindingList
        }
    }
}