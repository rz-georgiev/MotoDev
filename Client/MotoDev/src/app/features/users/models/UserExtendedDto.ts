import { UserDto } from "./userDto";

export interface UserExtendedDto extends UserDto {
    refreshToken: string;
}