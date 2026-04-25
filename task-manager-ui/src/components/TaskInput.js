import React, { useState } from 'react';
import {
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  TextField,
  DialogActions
} from "@mui/material";

export default function TaskInput({open, id, setHandleClose, handleAdd}) {
    const [taskName, setTaskName] = useState("");
    const [description, setDescription] = useState("");
    const [dueDate, setDueDate] = useState("");
    var canSubmit = taskName && description && dueDate;

    const handleAddTask = ()=>{
        console.log(taskName, description, dueDate, id);
        var task = {
            id:id,
            name: taskName,
            description: description,
            status: "pending",
            dueDate: dueDate
        }
        handleAdd(task);
        handleClose();
        // console.log(props.handlAdd);
    }

    const resetForm = () =>{
        setTaskName("");
        setDescription("");
        setDueDate("");
    }

    const handleClose = () => {
        resetForm();
        setHandleClose(false);
    }
  return (
    <Dialog open={open} onClose={handleClose}>
            <DialogTitle>Add Task</DialogTitle>

            <DialogContent>
            <TextField
                label="Task Name"
                fullWidth
                margin="dense"
                value={taskName}
                onChange={(e) => setTaskName(e.target.value)}
            />
            <TextField
                label="Description"
                fullWidth
                margin="dense"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
            />
            <TextField
                type="date"
                fullWidth
                margin="dense"
                InputLabelProps={{ shrink: true }}
                value={dueDate}
                onChange={(e) => setDueDate(e.target.value)}
            />
            </DialogContent>

            <DialogActions>
            <Button onClick={handleClose}>Cancel</Button>
            <Button onClick={handleAddTask} variant="contained">
                Save
            </Button>
            </DialogActions>
        </Dialog>
  )
}
