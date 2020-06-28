import { Component, OnInit } from '@angular/core';
import { ContactService } from '../shared/services/contact.service';
import { IContact } from '../shared/interfaces/i-contact';

@Component({
  selector: 'app-add-contact',
  templateUrl: './add-contact.component.html',
  styleUrls: ['./add-contact.component.css']
})
export class AddContactComponent implements OnInit {
  message: string = '';
  success: boolean;
  public contactForm: IContact = {
    firstName: '',
    lastName: '',
    age: null,
    address: '',
    interests: '',
    pictureName: ''
  };

  constructor(private contactService: ContactService) { }

  ngOnInit() {
  }

  //Add Contact event
  onAddContact() {
    //Get image path
    let firstChar = this.contactForm.firstName[0];
    this.contactForm.pictureName = `/img/${firstChar}.jpg`;
    console.log(this.contactForm);

    //Subscribe to addContact() service
    this.contactService.addContact(this.contactForm).subscribe(
      response => {
        if (response) {
          this.message = 'Contact Successfully Added!';
          this.success = true;
        }
      },
      error => {
        this.message = 'Failed to Add Contact!';
        this.success = false;
      }
    );
  }
}
