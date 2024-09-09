export interface CurrentUser {
    id: number;
    firstName: string;
    lastName: string;
    username: string;
    role: string | undefined;
}