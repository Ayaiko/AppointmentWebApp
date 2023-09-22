const API_URL = 'https://jsonplaceholder.typicode.com/posts'; // Replace with your API endpoint

// Initialize the calendar
function initializeCalendar(year, month) {
    const firstDayOfMonth = new Date(year, month, 1);
    const numberOfDaysInMonth = new Date(year, month + 1, 0).getDate();

    // Get the day of the week for the first day of the month (0 = Sunday, 1 = Monday, ..., 6 = Saturday)
    let startDayOfWeek = firstDayOfMonth.getDay();

    // Create the calendar grid HTML structure
    const calendarBody = document.getElementById('calendarBody');
    calendarBody.innerHTML = ''; // Clear any existing content

    let currentDate = new Date(firstDayOfMonth);

    // Add empty cells before the first day to align it correctly
    for (let i = 0; i < startDayOfWeek; i++) {
        const emptyCell = document.createElement('td');
        calendarBody.appendChild(emptyCell);
    }

    // Loop through the days of the month
    for (let i = 1; i <= numberOfDaysInMonth; i++) {
        // Create a cell for the day
        const cell = document.createElement('td');
        cell.textContent = i;

        // Add the cell to the calendar grid
        calendarBody.appendChild(cell);

        // Move to the next day
        currentDate.setDate(currentDate.getDate() + 1);

        // Start a new row on Sunday (dayOfWeek === 0)
        if (currentDate.getDay() === 0) {
            const newRow = document.createElement('tr');
            calendarBody.appendChild(newRow);
        }
    }

    // Get the name of the month
    const monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    const currentMonthName = monthNames[month];

    // Display the month and year in the <h2> element
    const currentYear = firstDayOfMonth.getFullYear();
    document.getElementById('currentMonthYear').textContent = `${currentMonthName} ${currentYear}`;
}

// Render events on the calendar
function renderAppointments() {
    fetch(API_URL)
        .then(response => response.json())
        .then(appointments => {
            // Loop through appointments and render them on the calendar grid
            appointments.forEach(appointment => {
                const date = new Date(appointment.date); // Replace 'date' with your date property name
                const dayCell = document.querySelector(`[data-date="${date.toISOString().split('T')[0]}"]`);

                if (dayCell) {
                    const appointmentDiv = document.createElement('div');
                    appointmentDiv.classList.add('appointment');
                    appointmentDiv.innerText = appointment.title; // Replace 'title' with your appointment title property name

                    // Append the appointment to the day cell
                    dayCell.appendChild(appointmentDiv);
                }
            });
        })
        .catch(error => console.error('Error fetching appointments:', error));
}


// Call functions to initialize and render the calendar
initializeCalendar(2023, 8);
// renderAppointments();
