import { CommonModule } from '@angular/common';
import { Component, Renderer2 } from '@angular/core';
import { NavbarService } from '../../../layout/header/services/navbar.service';
import { RedirectCommand } from '@angular/router';
import ApexCharts from 'apexcharts'
import { ViewChild } from "@angular/core";
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexDataLabels,
  ApexTooltip,
  ApexStroke,
  NgApexchartsModule
} from "ng-apexcharts";
import { DashboardService } from '../services/dashboard.service';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { DashboardResponse } from '../models/dashboardResponse';
import { RoleOption } from '../../../shared/consts/roleOption';
import { RepairStatusOption } from '../../../shared/consts/repairStatusOption';

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  stroke: ApexStroke;
  tooltip: ApexTooltip;
  dataLabels: ApexDataLabels;
};

@Component({
  selector: 'app-main-screen',
  standalone: true,
  imports: [CommonModule, NgApexchartsModule],
  templateUrl: './main-screen.component.html',
  styleUrl: './main-screen.component.css'
})


export class MainScreenComponent {

  @ViewChild("chart") chart!: ChartComponent;
  public chartOptions!: Partial<ChartOptions>;
  public dashboardResponse!: DashboardResponse;
  Math = Math;
  RepairStatusOption = RepairStatusOption;

  constructor(private navbarService: NavbarService,
    private dashboardService: DashboardService
  ) { }

  ngOnInit() {
    this.dashboardService.getDashboardData().subscribe(x => {
      this.dashboardResponse = x.result;

      this.chartOptions = {
        series: [
          {
            name: "Repairs",
            data: this.dashboardResponse.dashboardReports.repairs
          },
          {
            name: "Total Profit",
            data: this.dashboardResponse.dashboardReports.totalProfits
          }
        ],
        chart: {
          height: 350,
          type: "area"
        },
        dataLabels: {
          enabled: false
        },
        stroke: {
          curve: "smooth"
        },
        xaxis: {
          type: "datetime",
          labels: {
            format: 'MMM yyyy'
          },
          categories: this.dashboardResponse.dashboardReports.dates
        },
        tooltip: {
          x: {
            format: "yy/MM"
          }
        }
      };
    });

 
  }


}
