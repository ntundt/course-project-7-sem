export * from './attempt.service';
import { AttemptService } from './attempt.service';
export * from './authentication.service';
import { AuthenticationService } from './authentication.service';
export * from './contest.service';
import { ContestService } from './contest.service';
export * from './gradeAdjustments.service';
import { GradeAdjustmentsService } from './gradeAdjustments.service';
export * from './oidcConfiguration.service';
import { OidcConfigurationService } from './oidcConfiguration.service';
export * from './problem.service';
import { ProblemService } from './problem.service';
export * from './schemaDescription.service';
import { SchemaDescriptionService } from './schemaDescription.service';
export * from './scoreboard.service';
import { ScoreboardService } from './scoreboard.service';
export * from './user.service';
import { UserService } from './user.service';
export const APIS = [AttemptService, AuthenticationService, ContestService, GradeAdjustmentsService, OidcConfigurationService, ProblemService, SchemaDescriptionService, ScoreboardService, UserService];
