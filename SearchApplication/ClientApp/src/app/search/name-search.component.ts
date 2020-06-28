import { Component, OnInit } from '@angular/core';
import { IContact } from '../shared/interfaces/i-contact';
import { ContactService } from '../shared/services/contact.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-name-search',
  templateUrl: './name-search.component.html',
  styleUrls: ['./name-search.component.css']
})
export class NameSearchComponent implements OnInit {
  nameStr: string;
  contactList: IContact[];
  searchInProgress: boolean;

  constructor(private contactService: ContactService, private spinnerService: NgxSpinnerService ) { }

  ngOnInit() {
  }

  //Search event
  onSearch(){
    if (this.nameStr != null && this.nameStr != ''){
      this.spinnerService.show()

      this.contactService.getContacts(this.nameStr).subscribe(response => { 
          setTimeout(() => { 
            this.contactList = response;
            this.spinnerService.hide();
          }, 2000);
        },
        error => {
          console.log('GetContact() service failed.');
          this.spinnerService.hide();
          alert('GetContact() service failed.');
        }
      );
    }
  }
}
