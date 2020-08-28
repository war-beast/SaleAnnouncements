<template>
	<div class="d-flex flex-column">
		<h4 class="h4">Хотите купить платные статусы? <span class="text-muted">(поднимают объявление в общем списке)</span></h4>
		<div class="form-check my-2" v-for="status in statuses">
			<input class="form-check-input" type="checkbox" :name="paidStatus" v-model="paidStatus" :value="status.id" :id="status.id" v-on:change="selectionChanged">
			<label class="form-check-label" :for="status.id">
				{{status.name}}. Цена: {{status.price}} ₽
			</label>
		</div>

		<div v-show="paidStatus.length > 0">
			<b>Имитация формы оплаты:</b>
			<div class="w-100 pay-form d-flex align-items-end">
				<span class="shadow p-3 mb-5 bg-white rounded">Итоговая сумма на оплату: <b>{{totalAmount()}} ₽</b></span>
			</div>
		</div>
	</div>
</template>

<script src="./statusSelector.ts"></script>