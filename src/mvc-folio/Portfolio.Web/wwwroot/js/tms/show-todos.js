document.querySelectorAll('.jsTaskEntry').forEach(function (checkbox) {
    checkbox.addEventListener('change', function () {
        const taskId = this.id;
        const isCompleted = this.checked;
        console.log(this);
        // AJAX call
        fetch('/api/todos/status/toggle', {
            method: 'PATCH',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                uuid: taskId,
                isComplete: isCompleted
            })
        })
            .then(response => {
                if (!response.ok) throw new Error('Network response was not ok');
                return response.json();
            })
            .then(data => {
                console.log('Task updated successfully:', data);
            })
            .catch(error => {
                console.error('Error updating task:', error);
            });
    });
});

document.getElementById('btnAddTodo').addEventListener('click', function () {
    window.location.href = '/TaskManager/Todos/AddTodo';
});