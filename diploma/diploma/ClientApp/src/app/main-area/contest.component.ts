import { Component, OnInit } from '@angular/core';
import {faCheck, faClock, faCog, faDatabase, faListOl, faTasks, faUsers} from "@fortawesome/free-solid-svg-icons";
import {ActivatedRoute, Router} from "@angular/router";
import {PermissionsService} from "../../authorization/permissions.service";
import {ContestDto, ContestService} from "../../generated/client";
import * as moment from 'moment';
import { TranslateService } from '@ngx-translate/core';

interface SidebarItem {
  icon: any;
  text: string;
  route: string;
  requiresClaim?: string;
}

@Component({
  selector: 'app-main-area',
  templateUrl: './contest.component.html',
  styleUrls: ['./contest.component.css'],
})
export class ContestComponent implements OnInit {
  public listItems: Array<SidebarItem> = [
    {icon: faDatabase, text: 'sidebar.schemas', route: 'schemas', requiresClaim: 'ManageSchemaDescriptions'},
    {icon: faTasks, text: 'sidebar.problems', route: 'problems'},
    {icon: faCheck, text: 'sidebar.attempts', route: 'attempts'},
    {icon: faUsers, text: 'sidebar.participants', route: 'participants', requiresClaim: 'ManageContestParticipants'},
    {icon: faListOl, text: 'sidebar.scoreboard', route: 'scoreboard'},
    {icon: faCog, text: 'sidebar.settings', route: 'settings', requiresClaim: 'ManageContests'}
  ];

  private contestId: string = '';

  public contest: ContestDto | undefined;

  contestGoingOnUntil: Date = new Date();

  constructor(
    private route: ActivatedRoute,
    public permissionsService: PermissionsService,
    private contestsService: ContestService,
    private router: Router,
    private translateService: TranslateService,
  ) {
    this.onContestEnded = this.onContestEnded.bind(this);
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.contestId = params['contestId'];
    });
    this.contestsService.apiContestsGet().subscribe(res => {
      const contestId = this.route.snapshot.params['contestId'];
      this.contest = res.contests?.find(contest => contest.id === contestId);
      this.contestGoingOnUntil = new Date(Date.now() + (this.contest?.timeUntilFinishSeconds ?? 0) * 1000);
    });
  }

  onContestEnded() {
    if (!this.permissionsService.hasPermission('ManageContests')) {
      this.router.navigate(['/scoreboard', this.contestId]);
    }
  }

  public showTimer() {
    return new Date(this.contest?.startDate! + 'Z').getTime() < Date.now() 
      && new Date(this.contest?.finishDate! + 'Z').getTime() > Date.now();
  }

  protected readonly faClock = faClock;
}
