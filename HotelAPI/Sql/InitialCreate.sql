CREATE TABLE room_kinds
(
    id   SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE buildings
(
    id     SERIAL PRIMARY KEY,
    name   VARCHAR(30) NOT NULL UNIQUE,
    floors INT         NOT NULL
);

CREATE TABLE rooms
(
    id          SERIAL PRIMARY KEY,
    kind_id     INT NOT NULL,
    building_id INT NOT NULL,
    floor       INT NOT NULL,
    FOREIGN KEY (kind_id) REFERENCES room_kinds (id),
    FOREIGN KEY (building_id) REFERENCES buildings (id)
);

CREATE TABLE reservations
(
    id            SERIAL PRIMARY KEY,
    room_id       INT         NOT NULL,
    client_email  VARCHAR(50) NOT NULL,
    start_date    DATE        NOT NULL,
    end_date      DATE        NOT NULL,
    late_checkout BOOL        NOT NULL,
    FOREIGN KEY (room_id) REFERENCES rooms (id)
);

CREATE TABLE clients
(
    id      SERIAL PRIMARY KEY,
    name    VARCHAR(30) NOT NULL,
    UCN     VARCHAR(20) NOT NULL,
    room_id INT         NOT NULL,
    FOREIGN KEY (room_id) REFERENCES rooms (id)
);

CREATE TABLE parking
(
    id       SERIAL PRIMARY KEY,
    name     VARCHAR(20) NOT NULL,
    capacity INT
);

CREATE TABLE vehicles
(
    client_id    INT NOT NULL,
    parking_id   INT NOT NULL,
    registration VARCHAR(10) PRIMARY KEY,
    FOREIGN KEY (parking_id) REFERENCES parking (id),
    FOREIGN KEY (client_id) REFERENCES clients (id)
);