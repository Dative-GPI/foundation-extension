<template>
  <FSCol
    gap="24px"
  >
    <FSRow
      align="center-left"
      gap="32px"
      :wrap="false"
    >
      <FSSearchField
        :hideHeader="true"
        v-model="search"
      />
      <FSSwitch
        :label="$tr('ui.permissions-grid.select-all', 'Select all')"
        :modelValue="allSelected"
        @update:modelValue="onSelectAll"
      />
    </FSRow>
    <FSRow
      v-for="(grid, index) in grids"
      :key="index"
    >
      <FSCol
        gap="24px"
      >
        <FSRow
          align="center-left"
          gap="16px"
        >
          <FSSwitch
            :modelValue="categorySelected(grid.categoryCode)"
            @update:modelValue="onSelectCategory(grid.categoryCode)"
          />
          <FSText
            font="text-h3"
          >
            {{  grid.categoryLabel }}
          </FSText>
        </FSRow>
        <FSRow
          v-for="(item, index) in grid.items"
          align="center-left"
          gap="16px"
          :key="index"
        >
          <FSSwitch
            :label="item.label"
            :modelValue="permissionSelected(item.id)"
            @update:modelValue="onSelectPermission(item.id)"
          />
          <FSRow
            align="center-right"
          >
            <FSIconCheck
              :value="permissionSelected(item.id)"
            />
          </FSRow>
        </FSRow>
        <FSDivider />
      </FSCol>
    </FSRow>
  </FSCol>
</template>

<script lang="ts">
import { computed, defineComponent, onMounted, type PropType, ref } from "vue";

import { usePermissionOrganisations, usePermissionOrganisationCategories } from "../composables";

export default defineComponent({
  name: "EditPermissionsGrid",
  props: {
    modelValue: {
      type: Array as PropType<string[]>,
      required: false,
      default: () => []
    }
  },
  emits: ["update:modelValue"],
  setup(props, { emit }) {
    const { fetch: getManyPermissionOrganisationCategories, entity: permissionOrganisationCategories } = usePermissionOrganisationCategories();
    const { getMany: getManyPermissionOrganisations, entities: permissionOrganisations } = usePermissionOrganisations();

    const search = ref("");

    const grids = computed(() => {
      if (!permissionOrganisationCategories.value || !permissionOrganisations.value) {
        return [];
      }
      return permissionOrganisationCategories.value.map(pc => ({
        categoryLabel: pc.label,
        categoryCode: pc.prefix,
        items: permissionOrganisations.value.filter(op => op.code.startsWith(pc.prefix)).map(op => ({
          id: op.id,
          label: op.label,
          hideDefault: true
        }))
      })).reduce((acc, grid) => {
        let reducedGrid = { ...grid, items: [...grid.items] };
        if (search.value.length) {
          reducedGrid.items = reducedGrid.items
            .filter(item => JSON.stringify(item).toLowerCase().includes(search.value.toLowerCase()));
        }
        if (reducedGrid.items.length) {
          acc.push(reducedGrid);
        }
        return acc;
      }, []);
    });

    const allSelected = computed((): boolean => {
      if (permissionOrganisations.value) {
        return permissionOrganisations.value.every(p => props.modelValue.includes(p.id));
      }
      return false;
    });

    const onSelectAll = (): void => {
      if (permissionOrganisations.value) {
        if (allSelected.value) {
          emit("update:modelValue", []);
        }
        else {
          emit("update:modelValue", permissionOrganisations.value.map(p => p.id));
        }
      }
    };

    const categorySelected = (prefix: string): boolean => {
      if (permissionOrganisations.value) {
        const categoryIds = permissionOrganisations.value.filter(p => p.code.startsWith(prefix)).map(p => p.id);
        return categoryIds.every(id => props.modelValue.includes(id));
      }
      return false;
    };

    const onSelectCategory = (prefix: string): void => {
      if (permissionOrganisations.value) {
        const categoryIds = permissionOrganisations.value.filter(p => p.code.startsWith(prefix)).map(p => p.id);
        if (categorySelected(prefix)) {
          emit("update:modelValue", props.modelValue.filter(id => !categoryIds.includes(id)));
        }
        else {
          emit("update:modelValue", Array.from(new Set([...props.modelValue, ...categoryIds])));
        }
      }
    };

    const permissionSelected = (id: string): boolean => {
      if (permissionOrganisations.value) {
        const permission = permissionOrganisations.value.find(p => p.id === id);
        if (permission) {
          return props.modelValue.includes(permission.id);
        }
      }
      return false;
    };

    const onSelectPermission = (id: string): void => {
      if (permissionOrganisations.value) {
        const permission = permissionOrganisations.value.find(p => p.id === id);
        if (permission) {
          if (permissionSelected(id)) {
            emit("update:modelValue", props.modelValue.filter(p => p !== permission.id));
          }
          else {
            emit("update:modelValue", [...props.modelValue, permission.id]);
          }
        }
      }
    };

    onMounted(() => {
      getManyPermissionOrganisationCategories();
      getManyPermissionOrganisations();
    });

    return {
      allSelected,
      search,
      grids,
      onSelectPermission,
      permissionSelected,
      categorySelected,
      onSelectCategory,
      onSelectAll
    };
  }
});
</script>