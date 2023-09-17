interface PermissionDto {
  id?: number;
  employeeName: string;
  employeeLastName: string;
  permissionTypeId: number;
  permissionTypeDescription?: string;
  permissionDate?: Date;
}
