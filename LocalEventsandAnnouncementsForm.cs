using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace mary_tshabalala_MunicipalServicesApp
{
    public partial class LocalEventsandAnnouncementsForm : Form
    {
        private SortedDictionary<string, List<Event>> eventsByCategory;
        private HashSet<string> uniqueCategories;

        public LocalEventsandAnnouncementsForm()
        {
            InitializeComponent();
            uniqueCategories = new HashSet<string>();
            eventsByCategory = new SortedDictionary<string, List<Event>>();

            // Sample events
            AddEvent(new Event { Title = "Community Cleanup", Category = "Sanitation", DateTime = new DateTime(2024, 10, 25, 10, 0, 0), Description = "Join us for a community cleanup event." });
            AddEvent(new Event { Title = "Cooking Class", Category = "Agriculture", DateTime = new DateTime(2024, 11, 5, 18, 30, 0), Description = "Learn how to make delicious local cuisine." });
            AddEvent(new Event { Title = "Art Exhibition", Category = "sport and recreation", DateTime = new DateTime(2024, 11, 15, 12, 0, 0), Description = "Explore the work of local artists." });

            UpdateEventsList();
        }

        private void AddEvent(Event evt)
        {
            if (!eventsByCategory.ContainsKey(evt.Category))
            {
                eventsByCategory[evt.Category] = new List<Event>();
                uniqueCategories.Add(evt.Category);
            }
            eventsByCategory[evt.Category].Add(evt);
        }

        private void UpdateEventsList()
        {
            eventsListBox.Items.Clear();
            foreach (var categoryEvents in eventsByCategory.Values)
            {
                foreach (var evt in categoryEvents.OrderBy(e => e.DateTime))
                {
                    eventsListBox.Items.Add($"{evt.Title} - {evt.DateTime:MM/dd/yyyy HH:mm}");
                }
            }
        }

        private void LocalEventsandAnnouncementsForm_Load(object sender, EventArgs e)
        {
            // Populate categoryComboBox with unique categories
            categoryComboBox.Items.Clear();
            categoryComboBox.Items.AddRange(uniqueCategories.ToArray());
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string searchCategory = categoryTextBox.Text.Trim();
            DateTime startDate = startDatePicker.Value;
            DateTime endDate = endDatePicker.Value;

            var filteredEvents = new List<Event>();
            if (string.IsNullOrEmpty(searchCategory))
            {
                foreach (var categoryEventsList in eventsByCategory.Values)
                {
                    filteredEvents.AddRange(categoryEventsList.Where(evt => evt.DateTime >= startDate && evt.DateTime <= endDate));
                }
            }
            else if (eventsByCategory.TryGetValue(searchCategory, out var categoryEventsList))
            {
                filteredEvents.AddRange(categoryEventsList.Where(evt => evt.DateTime >= startDate && evt.DateTime <= endDate));
            }

            eventsListBox.Items.Clear();
            if (filteredEvents.Count == 0)
            {
                MessageBox.Show("No events found for the selected criteria.");
                return;
            }

            foreach (var evt in filteredEvents.OrderBy(ev => ev.DateTime))
            {
                eventsListBox.Items.Add($"{evt.Title} - {evt.DateTime:MM/dd/yyyy HH:mm}");
            }
        }

        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = (string)categoryComboBox.SelectedItem;
            var recommendedEvents = new List<Event>();

            foreach (var category in uniqueCategories)
            {
                if (category != selectedCategory)
                {
                    recommendedEvents.AddRange(eventsByCategory[category]);
                }
            }

            recommendedEventsListBox.Items.Clear();
            foreach (var recommendedEvent in recommendedEvents.OrderBy(evt => evt.DateTime).Take(5))
            {
                recommendedEventsListBox.Items.Add($"{recommendedEvent.Title} - {recommendedEvent.Category} - {recommendedEvent.DateTime:MM/dd/yyyy HH:mm}");
            }
        }

        private void recommendedEventsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}