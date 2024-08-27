import { Component, OnInit } from '@angular/core';
import { DataTablesModule } from "angular-datatables";
import { Config } from 'datatables.net';
import { UserResponse } from '../models/userResponse';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [DataTablesModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit {

  dtOptions: Config = {};

  ngOnInit() {
    // this.dtOptions = {
    //   serverSide: true, // Set the flag
    //   ajax: (dataTablesParameters: any, callback) => {
    //     that.http.post<UserResponse>("https://somedomain.com/api/1/data/", dataTablesParameters, {}).subscribe((resp) => {
    //       callback({
    //         recordsTotal: resp.recordsTotal,
    //         recordsFiltered: resp.recordsFiltered,
    //         data: resp.data,
    //       });
    //     });
    //   },
    //   columns: [
    //     {
    //       title: "ID",
    //       data: "id",
    //     },
    //     {
    //       title: "First name",
    //       data: "firstName",
    //     },
    //     {
    //       title: "Last name",
    //       data: "lastName",
    //     },
    //   ],
    // };
  }
}
