-- Schéma initial - Projet BTS SIO
-- Cible provisoire : PostgreSQL 15 ou version ultérieure.
-- Les mots de passe ne sont jamais stockés en clair.

BEGIN;

CREATE TABLE users (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    email VARCHAR(255) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE roles (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    code VARCHAR(50) NOT NULL UNIQUE,
    label VARCHAR(100) NOT NULL UNIQUE,
    description TEXT
);

CREATE TABLE permissions (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    code VARCHAR(100) NOT NULL UNIQUE,
    description TEXT NOT NULL
);

CREATE TABLE user_roles (
    user_id BIGINT NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    role_id BIGINT NOT NULL REFERENCES roles(id) ON DELETE RESTRICT,
    PRIMARY KEY (user_id, role_id)
);

CREATE TABLE role_permissions (
    role_id BIGINT NOT NULL REFERENCES roles(id) ON DELETE CASCADE,
    permission_id BIGINT NOT NULL REFERENCES permissions(id) ON DELETE RESTRICT,
    PRIMARY KEY (role_id, permission_id)
);

CREATE TABLE students (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    user_id BIGINT NOT NULL UNIQUE REFERENCES users(id) ON DELETE RESTRICT,
    internal_number VARCHAR(50) NOT NULL UNIQUE,
    birth_date DATE,
    phone VARCHAR(30),
    address TEXT
);

CREATE TABLE teachers (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    user_id BIGINT NOT NULL UNIQUE REFERENCES users(id) ON DELETE RESTRICT,
    internal_number VARCHAR(50) NOT NULL UNIQUE,
    phone VARCHAR(30),
    address TEXT
);

CREATE TABLE academic_years (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    label VARCHAR(20) NOT NULL UNIQUE,
    starts_on DATE NOT NULL,
    ends_on DATE NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT FALSE,
    CHECK (ends_on > starts_on)
);

CREATE TABLE classes (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    label VARCHAR(100) NOT NULL UNIQUE,
    level VARCHAR(100),
    capacity SMALLINT,
    CHECK (capacity IS NULL OR capacity > 0)
);

CREATE TABLE subjects (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    code VARCHAR(30) NOT NULL UNIQUE,
    label VARCHAR(100) NOT NULL UNIQUE,
    description TEXT
);

CREATE TABLE enrollments (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    student_id BIGINT NOT NULL REFERENCES students(id) ON DELETE RESTRICT,
    class_id BIGINT NOT NULL REFERENCES classes(id) ON DELETE RESTRICT,
    academic_year_id BIGINT NOT NULL REFERENCES academic_years(id) ON DELETE RESTRICT,
    enrolled_on DATE NOT NULL DEFAULT CURRENT_DATE,
    status VARCHAR(20) NOT NULL DEFAULT 'active',
    UNIQUE (student_id, academic_year_id),
    CHECK (status IN ('active', 'transferred', 'completed', 'cancelled'))
);

CREATE TABLE teaching_assignments (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    teacher_id BIGINT NOT NULL REFERENCES teachers(id) ON DELETE RESTRICT,
    class_id BIGINT NOT NULL REFERENCES classes(id) ON DELETE RESTRICT,
    subject_id BIGINT NOT NULL REFERENCES subjects(id) ON DELETE RESTRICT,
    academic_year_id BIGINT NOT NULL REFERENCES academic_years(id) ON DELETE RESTRICT,
    UNIQUE (teacher_id, class_id, subject_id, academic_year_id)
);

CREATE TABLE assessments (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    teaching_assignment_id BIGINT NOT NULL REFERENCES teaching_assignments(id) ON DELETE RESTRICT,
    label VARCHAR(150) NOT NULL,
    assessed_on DATE NOT NULL,
    coefficient NUMERIC(5,2) NOT NULL DEFAULT 1,
    max_score NUMERIC(6,2) NOT NULL DEFAULT 20,
    description TEXT,
    CHECK (coefficient > 0),
    CHECK (max_score > 0)
);

CREATE TABLE grades (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    assessment_id BIGINT NOT NULL REFERENCES assessments(id) ON DELETE RESTRICT,
    student_id BIGINT NOT NULL REFERENCES students(id) ON DELETE RESTRICT,
    score NUMERIC(6,2) NOT NULL,
    comment TEXT,
    entered_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UNIQUE (assessment_id, student_id),
    CHECK (score >= 0)
);

CREATE TABLE attendance_events (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    student_id BIGINT NOT NULL REFERENCES students(id) ON DELETE RESTRICT,
    event_type VARCHAR(20) NOT NULL,
    starts_at TIMESTAMPTZ NOT NULL,
    ends_at TIMESTAMPTZ,
    reason TEXT,
    justification_status VARCHAR(20) NOT NULL DEFAULT 'pending',
    justified_by BIGINT REFERENCES users(id) ON DELETE SET NULL,
    justified_at TIMESTAMPTZ,
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CHECK (event_type IN ('absence', 'late')),
    CHECK (justification_status IN ('pending', 'justified', 'rejected')),
    CHECK (ends_at IS NULL OR ends_at >= starts_at)
);

CREATE TABLE rooms (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    label VARCHAR(100) NOT NULL UNIQUE,
    location VARCHAR(150),
    capacity SMALLINT,
    CHECK (capacity IS NULL OR capacity > 0)
);

CREATE TABLE sessions (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    teaching_assignment_id BIGINT NOT NULL REFERENCES teaching_assignments(id) ON DELETE RESTRICT,
    room_id BIGINT REFERENCES rooms(id) ON DELETE SET NULL,
    starts_at TIMESTAMPTZ NOT NULL,
    ends_at TIMESTAMPTZ NOT NULL,
    title VARCHAR(150),
    CHECK (ends_at > starts_at)
);

CREATE TABLE documents (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    uploaded_by BIGINT NOT NULL REFERENCES users(id) ON DELETE RESTRICT,
    file_name VARCHAR(255) NOT NULL,
    storage_key VARCHAR(500) NOT NULL UNIQUE,
    mime_type VARCHAR(150) NOT NULL,
    file_size_bytes BIGINT NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CHECK (file_size_bytes > 0)
);

CREATE TABLE document_classes (
    document_id BIGINT NOT NULL REFERENCES documents(id) ON DELETE CASCADE,
    class_id BIGINT NOT NULL REFERENCES classes(id) ON DELETE CASCADE,
    PRIMARY KEY (document_id, class_id)
);

CREATE TABLE document_students (
    document_id BIGINT NOT NULL REFERENCES documents(id) ON DELETE CASCADE,
    student_id BIGINT NOT NULL REFERENCES students(id) ON DELETE CASCADE,
    PRIMARY KEY (document_id, student_id)
);

CREATE TABLE conversations (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    subject VARCHAR(200),
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    is_closed BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE conversation_participants (
    conversation_id BIGINT NOT NULL REFERENCES conversations(id) ON DELETE CASCADE,
    user_id BIGINT NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    joined_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    last_read_at TIMESTAMPTZ,
    PRIMARY KEY (conversation_id, user_id)
);

CREATE TABLE messages (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    conversation_id BIGINT NOT NULL REFERENCES conversations(id) ON DELETE CASCADE,
    author_id BIGINT NOT NULL REFERENCES users(id) ON DELETE RESTRICT,
    body TEXT NOT NULL,
    sent_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CHECK (length(trim(body)) > 0)
);

CREATE TABLE audit_logs (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    user_id BIGINT REFERENCES users(id) ON DELETE SET NULL,
    action VARCHAR(100) NOT NULL,
    resource_type VARCHAR(100) NOT NULL,
    resource_id VARCHAR(100),
    details JSONB,
    ip_address INET,
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_enrollments_class_year ON enrollments (class_id, academic_year_id);
CREATE INDEX idx_assignments_class_year ON teaching_assignments (class_id, academic_year_id);
CREATE INDEX idx_assessments_assignment ON assessments (teaching_assignment_id);
CREATE INDEX idx_grades_student ON grades (student_id);
CREATE INDEX idx_attendance_student_start ON attendance_events (student_id, starts_at);
CREATE INDEX idx_sessions_start ON sessions (starts_at);
CREATE INDEX idx_messages_conversation_sent ON messages (conversation_id, sent_at);
CREATE INDEX idx_audit_logs_created_at ON audit_logs (created_at);

COMMIT;
