import React from 'react'

export default function TaskCard(props) {
  const { id, name, description, status, dueDate } = props.task;
  console.log(id);
  var isDelayed = dueDate < new Date().toISOString().split('T')[0] && status !== "completed";
  return (
    <>
        <div className={`task-card ${isDelayed ? 'delayed-task' : ''}`}>
            <div className="task-card-header">
                {name}
            </div>
            <div className="task-card-body">
                <p><span className="title">Description:</span> {description}</p>
                <p><span className="title">Status:</span> {status}</p>
                <p><span className="title">Due On:</span> {dueDate}</p>
            </div>
        </div>
    </>
  )
}
