/**
 * diploma
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { AttemptStatus } from './attemptStatus';

export interface AttemptDto { 
    id?: string;
    problemId?: string;
    authorId?: string;
    status?: AttemptStatus;
    createdAt?: Date;
    errorMessage?: string;
    authorFirstName?: string;
    authorLastName?: string;
    authorPatronymic?: string;
    problemName?: string;
}