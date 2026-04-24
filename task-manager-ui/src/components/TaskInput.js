import React, { useState } from 'react'

export default function TaskInput(props) {
    const [taskName, setTaskName] = useState("");
    const [description, setDescription] = useState("");
    const [dueDate, setDueDate] = useState("");
    var canSubmit = taskName && description && dueDate;

    const handleAddTask = ()=>{
        console.log(taskName, description, dueDate, props.id);
        var task = {
            id:props.id,
            name: taskName,
            description: description,
            status: "pending",
            dueDate: dueDate
        }
        props.handleAdd(task);
        // console.log(props.handlAdd);
    }
  return (
    <div className="task-input">
        <input type="text" value={taskName} onChange={(e) => setTaskName(e.target.value)} placeholder="Task Name" />
        <input type="text" value={description} onChange={(e)=>{setDescription(e.target.value)}} placeholder="Description" />
        <input type="date" value={dueDate} onChange={(e)=>{setDueDate(e.target.value)}} placeholder="Due Date" />
        <button onClick={handleAddTask} disabled={!canSubmit}>Add Task</button>
    </div>
  )
}
