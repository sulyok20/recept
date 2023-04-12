
<template>
  <div>
    <h1>Receptek</h1>

    <div class="row row-cols-1 row-cols-md-2 g-4">
      <div class="col"
      v-for="(food, index) in foodWithCategrory"
          :key="`food${index}`">
        <div
          class="card"
         
        >
          <img :src="kep" class="card-img-top" alt="..." />
          <div class="card-body">
            <h5 class="card-title">{{ food.foodName }}</h5>
            <p class="card-text">
              <strong>Az étel feljegyzésének a dátuma:</strong>
              {{ food.descriptionDate }} <br />
              <strong>Az étel első készítésének dátuma:</strong>
              {{ food.firstDate }} <br />
              <strong>Kategória: </strong>
              <span
                v-for="(category, index) in food.category"
                :key="`category${index}`"
              >
                {{ category.categoryName }}
              </span>
            </p>
            <a href="#" class="btn btn-primary">Alapanyagok</a>
          </div>
        </div>
      </div>
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
      foodWithCategrory: [],
      kep: "../../public/káposztás tészta.jpg",
    };
  },
  mounted() {
    this.getfoodWithCategrory();
  },
  methods: {
    async getfoodWithCategrory() {
      let url = this.storeUrl.urlfoodWithCategrory;
      const config = {
        method: "GET",
        headers: {
          Authorization: `Bearer ${this.storeLogin.accessToken}`,
        },
      };
      const response = await fetch(url, config);
      const data = await response.json();
      this.foodWithCategrory = data.data;
    },
  },
};
</script>
<style>
</style>
