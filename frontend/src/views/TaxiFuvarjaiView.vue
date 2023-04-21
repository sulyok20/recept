
<template>
    <div class="p-3">
      <h1>Taxi fuvarjai</h1>
      <div v-for="(car, index) in listWithTrips" :key="`car${index}`">
        <h2>{{car.name}} ({{car.licenceNumber}}) {{car.hourlyRate}} Ft/óra</h2>
        <ul>
          <li  v-for="(trip, index) in car.trips" :key="`trip${index}`">
            {{trip.date}}: {{trip.numberOfMinits}} perc
          </li>
        </ul>
      </div>
    </div>
  </template>
  
  <script>
  import { useUrlStore } from "@/stores/url";
  import { useLoginStore } from "@/stores/login";
  const storeUrl = useUrlStore();
  const storeLogin = useLoginStore();
  export default {
    data() {
      return {
        storeUrl,
        storeLogin,
        listWithTrips: [],
      };
    },
    mounted(){
      this.getListCarsWithTrips()
    },
    methods: {
      async getListCarsWithTrips(){
        const url = storeUrl.urlCarsWithTrips;
        const config = {
          method: "GET",
          headers: {
            Authorization: `Bearer ${this.storeLogin.accessToken}`,
          },
        };
  
        const response = await fetch(url, config);
        const data = await response.json();
        this.listWithTrips = data.data;
        console.log(this.listWithTrips);
      },
    }
  }
  
  
  </script>
  
  
  <style>
  </style>