import React, { useState } from 'react'
import TaskCard from './TaskCard'
import TaskInput from './TaskInput'

export default function TaskList() {
    const taskList = [
        {
            id: 1,
            name: "my task1",
            description: "this is my task",
            status: "pending",
            dueDate: "2024-06-30"
        },
        {
            id:2,
            name: "my task2",
            description: "this is my task",
            status: "pending",
            dueDate: "2024-06-30"
        },
        {
            id:3,
            name: "my task3",
            description: "this is my task",
            status: "pending",
            dueDate: "2024-06-30"
        }
    ];

    const [tasks, setTasks] = useState(taskList);

    const HandleAddTask = (data) =>{
        setTasks([...tasks, data])
    }

    var max=tasks.reduce((prev, curr)=>{
            return curr.id>prev.id? curr:prev;
        });

        console.log("max=",max.id);

  return (
    <div className="task-list">
        
        <TaskInput handleAdd={HandleAddTask} id={max.id+1}></TaskInput>

        {
            tasks.map(task =>{
                return <TaskCard task={task} key={task.id}></TaskCard>
            })
        }
    </div>
    
  )
}
