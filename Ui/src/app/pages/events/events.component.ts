import { Component, OnInit, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { FormControl } from '@angular/forms';

import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

interface EventDto {
    id: number;
    title: string;
    date: Date;
    logo: string;
    place: string;
    available_tickets: number;
}

const EVENTS: EventDto[] = [
{
    id: 1,
    title: 'Event 1',
    date: new Date('2022-06-01T13:37:00'),
    logo: 'assets/images/events/event.png',
    place: 'Place 1',
    available_tickets: 123
},
{
    id: 2,
    title: 'Event 2',
    date: new Date('2022-06-02T13:37:00'),
    logo: 'assets/images/events/event.png',
    place: 'Place 2',
    available_tickets: 123
},
{
    id: 3,
    title: 'Event 3',
    date: new Date('2022-06-03T13:37:00'),
    logo: 'assets/images/events/event.png',
    place: 'Place 3',
    available_tickets: 123
},
{
    id: 4,
    title: 'Event 4',
    date: new Date('2022-06-21T13:37:00'),
    logo: 'assets/images/events/event.png',
    place: 'Place 4',
    available_tickets: 123
},
{
    id: 5,
    title: 'Event 5',
    date: new Date('2022-07-01T13:37:00'),
    logo: 'assets/images/events/event.png',
    place: 'Place 5',
    available_tickets: 123
}];

function search(text: string, pipe: PipeTransform): EventDto[] {
    return EVENTS.filter(event => {
        const term = text.toLowerCase();
        return event.title.toLowerCase().includes(term)
            || event.date.toDateString().includes(term)
            || event.place.toLowerCase().includes(term)
            || pipe.transform(event.available_tickets).includes(term);
    });
}

@Component({
    selector: 'ngbd-carousel-basic',
    templateUrl: './events.component.html',
    styleUrls: ['./events.component.css'],
    providers: [DecimalPipe]})

export class EventsComponent implements OnInit {

    events_week: Observable<EventDto[]>;
    events_month: Observable<EventDto[]>;
    events_all: Observable<EventDto[]>;

    filter = new FormControl('');

    constructor(pipe: DecimalPipe) {
        this.events_week = this.filter.valueChanges.pipe(startWith(''), map(text => search(text, pipe)));
        this.events_month = this.filter.valueChanges.pipe(startWith(''), map(text => search(text, pipe)));
        this.events_all = this.filter.valueChanges.pipe(startWith(''), map(text => search(text, pipe)));
    }

    ngOnInit(): void {
    }
}
