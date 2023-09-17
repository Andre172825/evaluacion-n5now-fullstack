import api from "./api";

export const getAllPermissionTypes = async () => {
  try {
    const response = await api.get("/permissionTypes");
    return response.data;
  } catch (error) {
    throw error;
  }
};
