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
import { TimeSpan } from './timeSpan';

export interface ProblemDto { 
    id?: string;
    name?: string;
    statement?: string;
    orderMatters?: boolean;
    floatMaxDelta?: number;
    caseSensitive?: boolean;
    timeLimit?: TimeSpan;
    maxGrade?: number;
    ordinal?: number;
    schemaDescriptionId?: string;
    availableDbms?: Array<string>;
}