# PBL4-HEROS

## MongoDB 구조

### Cells

| Field             | Type               |
|-------------------|--------------------|
| datetime          | String             |
| cellID            | String             |
| population_size   | Integer            |
| age_distribution  | Embedded Object    |
| statistics        | Embedded Object    |
| event             | Embedded Object    |
| people[]          | Array of Embedded Objects |

#### Age Distribution (Embedded Object)

| age   | count |
|-------|-------|
| Integer | Integer |

#### Statistics (Embedded Object)

| average_age | Float |
|-------------|-------|
| median_age  | Float |

#### Event (Embedded Object)

| Field           | Type             |
|-----------------|------------------|
| name            | String           |
| event_date      | String (ISO 8601)|
| event_location  | String           |

#### People (Array of Embedded Objects)

| Field            | Type            |
|------------------|-----------------|
| peopleID         | String          |
| gender           | String          |
| age              | Integer         |
| movement_direction| Array of Float  |
| movement_speed   | Float           |
| location         | Object          |
| mobile_number    | String          |
| IMSI             | String          |

##### Location (Nested Object inside People)

| Field    | Type   |
|----------|--------|
| latitude | Float  |
| longitude| Float  |

