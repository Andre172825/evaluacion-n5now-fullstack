import api from "./api";

export const createPermission = async (
  permissionRequest: PermissionDto
): Promise<PermissionDto> => {
  try {
    const response = await api.post("/permissions", permissionRequest);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const updatePermission = async (
  permissionRequest: PermissionDto
): Promise<PermissionDto> => {
  try {
    const response = await api.put(
      `/permissions/${permissionRequest.id}`,
      permissionRequest
    );
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const deletePermission = async (
  permissionId: number
): Promise<boolean> => {
  try {
    const response = await api.delete(`/permissions/${permissionId}`);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const getAllPermissions = async (): Promise<PermissionDto[]> => {
  try {
    const response = await api.get("/permissions");
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const getPermissionById = async (
  permissionId: number
): Promise<PermissionDto> => {
  try {
    const response = await api.get(`/permissions/${permissionId}`);
    return response.data;
  } catch (error) {
    throw error;
  }
};
