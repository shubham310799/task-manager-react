import { getToken } from "./UserService";

const BASE_URL = "https://localhost:7131/api/task";

export const getTasks = async () => {
    const token = await getToken();
    const response = await fetch(`${BASE_URL}/all`, {
        headers: {
            "Authorization": `Bearer ${token}`,
        },
    });
    if (!response.ok) throw new Error("Failed to fetch tasks");
    const body = await response.json();
    if (body.success) {
        return body.data;
    } else {
        throw new Error(body.error.message || "Failed to fetch tasks");
    }
};

export const addTask = async (task) => {
    const token = await getToken();
    const response = await fetch(`${BASE_URL}/add`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`,
        },
        body: JSON.stringify(task),
    });

    if (!response.ok) throw new Error("Failed to add task");
    const body = await response.json();
    if (body.success) {
        return body.data;
    } else {
        throw new Error(body.error.message || "Failed to add task");
    }
};

export const updateTask = async (task) => {
    const token = await getToken();
    const response = await fetch(`${BASE_URL}/update`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`,
        },
        body: JSON.stringify(task),
    });

    if (!response.ok) throw new Error("Failed to update task");
    const body = await response.json();
    if (body.success) {
        return body.data;
    } else {
        throw new Error(body.error.message || "Failed to update task");
    }
};

export const deleteTask = async (id) => {
    const token = await getToken();
    const response = await fetch(`${BASE_URL}/${id}`, {
        method: "DELETE",
        headers: {
            "Authorization": `Bearer ${token}`,
        },
    });

    if (!response.ok) throw new Error("Failed to delete task");
    const body = await response.json();
    if (body.success) {
        return body.data;
    } else {
        throw new Error(body.error.message || "Failed to delete task");
    }
};