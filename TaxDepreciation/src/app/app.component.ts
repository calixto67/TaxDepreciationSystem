import { ChangeDetectorRef, Component, OnInit } from "@angular/core";
import { ContactService } from "./services/contact.service";
import { Contact } from "./models/contact.model";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  
  Contacts: Array<Contact> = [];
  title = 'TaxDepreciationSystem';
  contactForm!: FormGroup;
  contact!: Contact;
  editingItem: Contact | null = null;
  searchText?: string;
  showForm:boolean = false;
  /**
   *
   */
  constructor(
    public dataService: ContactService,
    private fb: FormBuilder,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
      this.showForm=false;
      this.createFormGroup();
      this.fetchData();
  }

  fetchData(){
    this.showForm=false;
    this.dataService.getContacts()
    .subscribe((res) => {
      this.Contacts = res.data;
      this.cdr.markForCheck();
    },
    (error) => {
      // Handle the error here
      alert(error.error);
    })
  }


  createFormGroup() {
    this.contactForm = this.fb.group({
      id: new FormControl(0, [Validators.required]),
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      companyName: new FormControl(''),
      mobile: new FormControl(''),
      email: new FormControl(''),
    });
  }

  editItem(item: Contact): void {
    this.contactForm.patchValue({
      firstName: item.firstName,
      lastName: item.lastName,
      companyName: item.companyName,
      mobile: item.mobile,
      email: item.email
    });
  
    this.editingItem = item;
  }

  submitForm(form: any): void {
    if (this.contactForm.valid) {
      if (this.editingItem) {
        // Update existing item
        this.editingItem.firstName = this.contactForm.value.firstName;
        this.editingItem.lastName = this.contactForm.value.lastName;
        this.editingItem.companyName = this.contactForm.value.companyName;
        this.editingItem.mobile = this.contactForm.value.mobile;
        this.editingItem.email = this.contactForm.value.email;
        this.dataService.saveContact({ ...this.editingItem})
                        .subscribe((res) => {
                          alert(res.status);
                          this.fetchData();
                        },
                        (error) => {
                          // Handle the error here
                          alert(error.error);
                        });
        this.editingItem = null;
      } else {
        // Create new contact
        this.dataService.saveContact({ ...this.contactForm.value})
                        .subscribe((res) =>{
                          alert(res.status);
                          this.fetchData();
                        },(error) => {
                          // Handle the error here
                          alert(error.error);
                        });
      }
      // Clear form fields
      this.contactForm.reset();
    }
  }

  deleteItem(item: Contact): void {
    const index = this.Contacts.indexOf(item);
    if (index !== -1) {
      this.Contacts.splice(index, 1);
    }
  }

  searchContact(e:any){
    if(e){
      this.dataService.getContacts(e?.currentTarget?.value)
                      .subscribe((res) => {
                        this.Contacts = res.data;
                      },
                      (error) => {
                        // Handle the error here
                        alert(error.error);
                      });
      this.cdr.markForCheck();
    }
  }

}
