import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router'
import { Observable, of } from "rxjs";

class EventDto {
    id: number;
    title: string;
    date: Date;
    place: string;

    constructor(id: number, title: string, date: Date, place: string) {
        this.id = id;
        this.title = title;
        this.date = date;
        this.place = place;
    }
}

interface TicketTypeDto {
    id: number;
    group_description: string;
    price: number;
    amount: number;
    available_tickets: number;
}

const TICKET_TYPES: TicketTypeDto[] = [
{
    id: 1,
    group_description: 'Arena',
    price: 30,
    amount: 1,
    available_tickets: 100
},
{
    id: 2,
    group_description: 'VIP',
    price: 60,
    amount: 1,
    available_tickets: 30
}];

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.css']
})

export class EventTicketsComponent implements OnInit {

    id: string | null;
    event: EventDto;
    ticket_types:  Observable<TicketTypeDto[]>;

    constructor(private route: ActivatedRoute) {
        this.id = this.route.snapshot.paramMap.get('id');
        this.event = new EventDto(1, 'Event 1', new Date('2022-6-30T13:37:00'), 'ekei');
        this.ticket_types = of(TICKET_TYPES);
    }

    ngOnInit(): void {
        this.id = this.route.snapshot.paramMap.get('id');
    }
}
