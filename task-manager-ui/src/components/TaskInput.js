import React, { useState } from 'react';
import {
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  TextField,
  DialogActions,
  InputLabel,
  Select,
  MenuItem
} from "@mui/material";

export default function TaskInput({open, currTask, setHandleClose, handleUpdate, isEdit}) {
    const [task, setTask] = useState(currTask);
    var canSubmit = task.name && task.description && task.dueDate && task.status;
    const handleUpdateTask = ()=>{
        var updatedTask = {
            id:task.id,
            name: task.name,
            description: task.description,
            status: task.status,
            dueDate: task.dueDate
        }
        handleUpdate(updatedTask);
        handleClose();
        // console.log(props.handlAdd);
    }

    const handleClose = () => {
        resetForm();
        setHandleClose(false);
    }

    const resetForm = () => {
        !isEdit && setTask({
            name: "",
            description: "",
            status: "pending",
            dueDate: ""
        });
    };
  return (
    <Dialog open={open} onClose={handleClose}>
            <DialogTitle>Add Task</DialogTitle>

            <DialogContent>
            <TextField
                label="Task Name"
                fullWidth
                margin="dense"
                value={task.name}
                onChange={(e) => setTask({...task, name: e.target.value})}
            />
            <TextField
                label="Description"
                fullWidth
                margin="dense"
                value={task.description}
                onChange={(e) => setTask({...task, description: e.target.value})}
            />
            <TextField
                type="date"
                fullWidth
                margin="dense"
                value={task.dueDate}
                onChange={(e) => setTask({...task, dueDate: e.target.value})}
            />

            <InputLabel id="demo-simple-select-label">Status</InputLabel>
                <Select
                    labelId="demo-simple-select-label"
                    id="demo-simple-select"
                    value={task.status}
                    label="Status"
                    onChange={(e) => setTask({...task, status: e.target.value})}
                >
                    <MenuItem value="completed">Completed</MenuItem>
                    <MenuItem value="in-progress">In Progress</MenuItem>
                    <MenuItem value="pending">Pending</MenuItem>
                </Select>
            </DialogContent>

            <DialogActions>
                <Button onClick={handleClose}>Cancel</Button>
                <Button disabled={!canSubmit} onClick={handleUpdateTask} variant="contained">
                    Save
                </Button>
            </DialogActions>
        </Dialog>
  )
}
